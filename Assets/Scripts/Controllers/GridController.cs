using System.Collections.Generic;
using Abstracts;
using Adapters;
using Cysharp.Threading.Tasks;
using Data;
using Data.Models;
using EventBus.Events;
using EventBusSystem;
using Extensions;
using Interfaces;
using Interfaces.Controllers;
using Miscs;

namespace Controllers
{
    public class GridController : IGridController
    {
        private readonly IGridData _gridData;
        private readonly IObjectFactory _objectFactory;
        private readonly ILevelData _currentLevelData;
        private readonly ITransform _parentTransform;
        private readonly ILogger _logger;

        public ITile[,] Tiles { get; private set; }
        public Dictionary<IVector2Int, ITile> TilesDict { get; set; }

        private readonly Dictionary<TileDirectionType, IVector2Int> _tileDirectionCoordinates = new()
        {
            { TileDirectionType.Up, new Vector2Int(0, 1) },
            { TileDirectionType.RightUp, new Vector2Int(1, 1) },
            { TileDirectionType.Right, new Vector2Int(1, 0) },
            { TileDirectionType.RightDown, new Vector2Int(1, -1) },
            { TileDirectionType.Down, new Vector2Int(0, -1) },
            { TileDirectionType.LeftDown, new Vector2Int(-1, -1) },
            { TileDirectionType.LeftUp, new Vector2Int(-1, 1) },
            { TileDirectionType.Left, new Vector2Int(-1, 0) }
        };

        private readonly Dictionary<TileDirectionType, IVector2Int> _tileAboveDirectionCoordinates = new()
        {
            { TileDirectionType.Up, new Vector2Int(0, 1) },
            { TileDirectionType.RightUp, new Vector2Int(1, 1) },
            { TileDirectionType.LeftUp, new Vector2Int(-1, 1) },
        };

        private readonly Dictionary<TileDirectionType, IVector2Int> _tileBelowDirectionCoordinates = new()
        {
            { TileDirectionType.Down, new Vector2Int(0, -1) },
            { TileDirectionType.RightDown, new Vector2Int(1, -1) },
            { TileDirectionType.LeftDown, new Vector2Int(-1, -1) },
        };

        public GridController(IGridData gridData, IObjectFactory objectFactory, ILevelData currentLevelData, ITransform parentTransform, ILogger logger)
        {
            _gridData = gridData;
            _objectFactory = objectFactory;
            _currentLevelData = currentLevelData;
            _parentTransform = parentTransform;
            _logger = logger;

            Tiles = new ITile[_currentLevelData.GridSize.x, _currentLevelData.GridSize.y + 1];
            TilesDict = new Dictionary<IVector2Int, ITile>();
            AddEventListeners();
        }

        public void AddEventListeners()
        {
            EventBusNew.SubscribeWithResult<TryToCheckAnyLinkExistEvent, UniTask>(TryToCheckAnyLinkExist);
        }

        public void RemoveEventListeners()
        {
            EventBusNew.UnsubscribeWithResult<TryToCheckAnyLinkExistEvent, UniTask>(TryToCheckAnyLinkExist);
        }

        public async void CreateGrid()
        {
            for (int x = 0; x < _currentLevelData.GridSize.x; x++)
            {
                for (int y = 0; y < _currentLevelData.GridSize.y + 1; y++)
                {
                    IVector3 tileSpawnPosition = new Vector3Adapter(new Vector3(x, y, 0));
                    IVector2Int tileCoordinates = new Vector2IntAdapter(new Vector2Int(x, y));

                    bool isSpawner = y == _currentLevelData.GridSize.y;
                    var tile = _objectFactory.CreateObject<ITile>(_gridData.TilePrefab, _parentTransform, tileSpawnPosition);
                    BasePlayableTileElement spawnedTileElement = null;
                    if (!isSpawner)
                        spawnedTileElement = await CreateTileElement(tile);

                    tile.Init(tileCoordinates, spawnedTileElement, isSpawner);

                    Tiles[tileCoordinates.x, tileCoordinates.y] = tile;
                    TilesDict.Add(new Vector2Int(tileCoordinates.x, tileCoordinates.y), tile);
                }
            }

            CalculateTileNeighbours();
        }
        private async UniTask<BasePlayableTileElement> CreateTileElement(ITile tile)
        {
            var randomPlayableTileElement = PlayableEntityType.Type1.GetRandom();
            var tileElementModel = new ElementModel((GameElementType)randomPlayableTileElement);
            var spawnParameters = new SpawnGameplayElementPoolEvent(tileElementModel,
                tile,
                new Vector3Adapter(Vector3.zero),
                new QuaternionAdapter(UnityEngine.Quaternion.identity.ToDataQuaternion()),
                tile.Transform);
            //var spawnedTileElement = await EventBus<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>.Raise(spawnParameters)[0];
            var spawnedTileElement = await EventBusNew.RaiseWithResult<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>(spawnParameters);
            return spawnedTileElement;
        }

        public void CalculateTileNeighbours()
        {
            foreach (var tile in TilesDict.Values)
            {
                var neighbours = new Dictionary<TileDirectionType, ITile>();
                var aboveNeighbours = new Dictionary<TileDirectionType, ITile>();
                var belowNeighbours = new Dictionary<TileDirectionType, ITile>();

                foreach (var direction in _tileDirectionCoordinates)
                {
                    var neighbourCoords = tile.Coordinates.Add(direction.Value);
                    if (TilesDict.TryGetValue(neighbourCoords, out var neighbourTile))
                    {
                        neighbours.Add(direction.Key, neighbourTile);

                        if (_tileAboveDirectionCoordinates.ContainsKey(direction.Key))
                            aboveNeighbours.Add(direction.Key, neighbourTile);
                        else if (_tileBelowDirectionCoordinates.ContainsKey(direction.Key))
                            belowNeighbours.Add(direction.Key, neighbourTile);
                    }
                }

                tile.Neighbours = neighbours;
                tile.AboveNeighbours = aboveNeighbours;
                tile.BelowNeighbours = belowNeighbours;
            }
        }

        private async UniTask TryToCheckAnyLinkExist(TryToCheckAnyLinkExistEvent @event)
        {
            var linkGroups = FindTileLinkGroups();
            if (linkGroups.Count > 0)
                return;

            await ShuffleTileElements();
        }

        private async UniTask ShuffleTileElements()
        {
            const int maxTries = 20;
            var tries = 0;

            var tileList = new List<ITile>();
            foreach (var tile in TilesDict.Values)
            {
                if (tile.IsSpawner || tile.TileElement == null)
                    continue;

                tileList.Add(tile);
            }

            do
            {
                tries++;
                var elements = new List<ITileElement>();
                foreach (var tile in tileList)
                {
                    elements.Add(tile.TileElement);
                    tile.TileElement.SetTile(null); 
                    tile.TileElement = null;
                }

                elements.Shuffle(); 

                for (int i = 0; i < tileList.Count; i++)
                {
                    var tile = tileList[i];
                    var element = elements[i];

                    tile.SetTileElement(element);
                    element.SetTile(tile);
                }

                var matchGroups = FindTileLinkGroups();

                if (matchGroups.Count > 0)
                {
                    _logger.Log($"Shuffle completed with match in {tries} tries.");
                    return;
                }

            } while (tries < maxTries);

            _logger.LogError("Shuffle failed to produce a valid match after max attempts.");
        }
        
        private List<List<ITile>> FindTileLinkGroups()
        {
            var visited = new HashSet<ITile>();
            var matchGroups = new List<List<ITile>>();

            foreach (var tile in TilesDict.Values)
            {
                if (visited.Contains(tile) || tile.TileElement == null)
                    continue;

                var currentGroup = new List<ITile>();
                FloodFill(tile, tile.TileElement.ElementType, currentGroup, visited);

                if (currentGroup.Count >= 3)
                    matchGroups.Add(currentGroup);
            }

            return matchGroups;
        }

        private void FloodFill(ITile tile, GameElementType matchType, List<ITile> group, HashSet<ITile> visited)
        {
            if (tile == null || visited.Contains(tile))
                return;

            if (tile.TileElement == null || tile.TileElement.ElementType != matchType)
                return;

            visited.Add(tile);
            group.Add(tile);

            foreach (var neighbour in tile.Neighbours.Values)
                FloodFill(neighbour, matchType, group, visited);
        }
    }
}