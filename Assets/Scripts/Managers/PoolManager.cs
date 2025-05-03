using Abstracts;
using Cysharp.Threading.Tasks;
using EventBus.Events;
using EventBusSystem;
using Pools;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private PlayableTileElementPool _playableElementPool;


        private void OnEnable()
        {
            EventBusNew.SubscribeWithResult<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>(TrySpawnPlayableTileElementPool);
            EventBusNew.Subscribe<ReturnToPoolEvent<BasePlayableTileElement>>(TryReturnToPlayableTileElementPool);
        }

        private void OnDestroy()
        {
            EventBusNew.UnsubscribeWithResult<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>(TrySpawnPlayableTileElementPool);
            EventBusNew.Unsubscribe<ReturnToPoolEvent<BasePlayableTileElement>>(TryReturnToPlayableTileElementPool);
        }

        private async UniTask<BasePlayableTileElement> TrySpawnPlayableTileElementPool(SpawnGameplayElementPoolEvent spawnGameplayElementPoolEvent)
        {
            var spawnedElement = _playableElementPool.SpawnFromPool(spawnGameplayElementPoolEvent.ElementModel.ElementType,
                spawnGameplayElementPoolEvent.Position,
                spawnGameplayElementPoolEvent.Rotation,
                spawnGameplayElementPoolEvent.Parent);
            await spawnedElement.Init(spawnGameplayElementPoolEvent.ElementModel, spawnGameplayElementPoolEvent.Tile);
            return spawnedElement;
        }

        private void TryReturnToPlayableTileElementPool(ReturnToPoolEvent<BasePlayableTileElement> returnToPoolEvent)
        {
            returnToPoolEvent.MonoBehaviour.gameObject.SetActive(false);
            _playableElementPool.ReturnToPool(returnToPoolEvent.ElementType, returnToPoolEvent.MonoBehaviour);
        }
    }
}