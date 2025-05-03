using Abstracts;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events;
using Pools;
using UnityEngine;

namespace Managers
{
    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private PlayableTileElementPool _playableElementPool;


        private void OnEnable()
        {
            EventBusManager.SubscribeWithResult<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>(TrySpawnPlayableTileElementPool);
            EventBusManager.Subscribe<ReturnToPoolEvent<BasePlayableTileElement>>(TryReturnToPlayableTileElementPool);
        }

        private void OnDestroy()
        {
            EventBusManager.UnsubscribeWithResult<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>(TrySpawnPlayableTileElementPool);
            EventBusManager.Unsubscribe<ReturnToPoolEvent<BasePlayableTileElement>>(TryReturnToPlayableTileElementPool);
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