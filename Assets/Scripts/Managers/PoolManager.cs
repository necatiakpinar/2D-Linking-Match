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

        private EventBinding<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>> _spawnFromPlayableTileElementPoolEventBinding;
        private EventBinding<ReturnToPoolEvent<BasePlayableTileElement>> _returnToPlayableTileElementPoolEventBinding;


        private void OnEnable()
        {
            _spawnFromPlayableTileElementPoolEventBinding =
                new EventBinding<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>(TrySpawnPlayableTileElementPool);
            _returnToPlayableTileElementPoolEventBinding = new EventBinding<ReturnToPoolEvent<BasePlayableTileElement>>(TryReturnToPlayableTileElementPool);

            EventBus<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>.Register(_spawnFromPlayableTileElementPoolEventBinding);
            EventBus<ReturnToPoolEvent<BasePlayableTileElement>>.Register(_returnToPlayableTileElementPoolEventBinding);
        }

        private void OnDestroy()
        {
            EventBus<SpawnGameplayElementPoolEvent, UniTask<BasePlayableTileElement>>.Deregister(_spawnFromPlayableTileElementPoolEventBinding);
            EventBus<ReturnToPoolEvent<BasePlayableTileElement>>.Deregister(_returnToPlayableTileElementPoolEventBinding);
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