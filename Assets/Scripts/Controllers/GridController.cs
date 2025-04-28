using System.Collections.Generic;
using Abstracts;
using Adapters;
using Cysharp.Threading.Tasks;
using Data;
using Data.Models;
using EventBus;
using EventBus.Events;
using Extensions;
using Interfaces;
using Miscs;

namespace Controllers
{
    public class GridController
    {
        private readonly IGridData _gridData;
        private readonly IObjectFactory _objectFactory;
        private readonly ILevelData _currentLevelData;
        private readonly ITransform _parentTransform;
        private readonly ILogger _logger;

        public ITile[,] Tiles { get; private set; }
        public Dictionary<Vector2Int, ITile> TilesDict { get; private set; }

        private readonly Dictionary<TileDirectionType, IVector2Int> _tileDirectionCoordinates = new()
        {
            { TileDirectionType.Up, new Vector2Int(0, 1) },
            { TileDirectionType.Right, new Vector2Int(1, 0) },
            { TileDirectionType.Down, new Vector2Int(0, -1) },
            { TileDirectionType.Left, new Vector2Int(-1, 0) }
        };

        public GridController(IGridData gridData, IObjectFactory objectFactory, ILevelData currentLevelData, ITransform parentTransform, ILogger logger)
        {
            _gridData = gridData;
            _objectFactory = objectFactory;
            _currentLevelData = currentLevelData;
            _parentTransform = parentTransform;
            _logger = logger;

            Tiles = new ITile[_currentLevelData.GridSize.x, _currentLevelData.GridSize.y];
            TilesDict = new Dictionary<Vector2Int, ITile>();
        }

        public async void CreateGrid()
        {
            for (int x = 0; x < _currentLevelData.GridSize.x; x++)
            {
                for (int y = 0; y < _currentLevelData.GridSize.y; y++)
                {
                    IVector3 tileSpawnPosition = new Vector3Adapter(new Vector3(x, y, 0));
                    IVector2Int tileCoordinates = new Vector2IntAdapter(new Vector2Int(x, y));

                    var tile = _objectFactory.CreateObject<ITile>(_gridData.TilePrefab, _parentTransform, tileSpawnPosition);

                    var randomPlayableTileElement = PlayableEntityType.Type1.GetRandom();
                    var tileElementModel = new ElementModel((GameElementType)randomPlayableTileElement);
                    var spawnParameters = new SpawnGameplayElementPoolEvent(tileElementModel,
                        new Vector3Adapter(Vector3.zero),
                        UnityEngine.Quaternion.identity, //todo: convert this IQuaternion
                        tile.Transform);
                    var spawnedTileElement = await EventBus<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>.Raise(spawnParameters)[0];

                    await tile.Init(tileCoordinates, spawnedTileElement);
                    Tiles[tileCoordinates.x, tileCoordinates.y] = tile;
                    TilesDict.Add(new Vector2Int(tileCoordinates.x, tileCoordinates.y), tile);
                }
            }

            CalculateTileNeighbours();
        }

        private void CalculateTileNeighbours()
        {
            var neighbours = new Dictionary<TileDirectionType, ITile>();
            var directionCount = System.Enum.GetValues(typeof(TileDirectionType)).Length;

            foreach (var tile in TilesDict.Values)
            {
                neighbours = new Dictionary<TileDirectionType, ITile>();
                for (int i = 0; i < directionCount; i++)
                {
                    var direction = (TileDirectionType)i;
                    var possibleNeighbourCoordinates = tile.Coordinates.Add(_tileDirectionCoordinates[direction]);
                    if (TilesDict.TryGetValue(possibleNeighbourCoordinates, out var neighbourTile))
                        neighbours.Add(direction, neighbourTile);
                }

                tile.SetNeighbours(neighbours);
            }
        }
    }
}