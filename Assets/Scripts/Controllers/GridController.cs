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

        public GridController(IGridData gridData, IObjectFactory objectFactory, ILevelData currentLevelData, ITransform parentTransform, ILogger logger)
        {
            _gridData = gridData;
            _objectFactory = objectFactory;
            _currentLevelData = currentLevelData;
            _parentTransform = parentTransform;
            _logger = logger;
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
                }

            }
        }

    }
}