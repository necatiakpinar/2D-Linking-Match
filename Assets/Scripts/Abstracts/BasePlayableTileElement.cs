using Addressables;
using Cysharp.Threading.Tasks;
using Data.Models;
using DG.Tweening;
using EventBus;
using EventBus.Events;
using Helpers;
using Interfaces;
using UnityEngine;
using UnityEngine.U2D;

namespace Abstracts
{
    public class BasePlayableTileElement : BaseTileElement, IPlayableTileElement, IPoolable<BasePlayableTileElement>
    {
        private Sequence _highlightSequence;
        
        public async override UniTask Init(ElementModel pElementModel, ITile tileMono)
        {
            await base.Init(pElementModel, tileMono);
            EnableSpriteRenderer();
            var spriteAtlas = await AddressablesLoader.LoadAssetAsync<SpriteAtlas>(AddressablesKeys.GetKey(AddressablesKeys.AssetKeys.SA_Set1TileElements));
            if (spriteAtlas == null)
            {
                LoggerUtil.LogError($"SpriteAtlas not found for {ElementType}");
                return;
            }

            _spriteRenderer.sprite = spriteAtlas.GetSprite(ElementType.ToString());
        }

        public async override UniTask Select()
        {
            await base.Select();
            if (_highlightSequence != null && _highlightSequence.IsActive())
                _highlightSequence.Kill();

            _highlightSequence = DOTween.Sequence();

            _highlightSequence.Append(transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.InOutSine))
                .Append(transform.DOScale(Vector3.one * 0.9f, 0.2f).SetEase(Ease.InOutSine))
                .Append(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutSine));

            _highlightSequence.SetLoops(-1, LoopType.Restart);
        }

        public async override UniTask Deselect()
        {
            await base.Deselect();
            if (_highlightSequence != null && _highlightSequence.IsActive())
                _highlightSequence.Kill();

            transform.localScale = Vector3.one;
        }

        public async override UniTask TryToActivate()
        {
            await base.TryToActivate();
            await Activate();
        }

        public async override UniTask Activate()
        {
            await base.Activate();
            await PlayDestroy();
        }

        public void ReturnToPool(BasePlayableTileElement poolObject)
        {
            TileMono = null;
            EventBus<ReturnToPoolEvent<BasePlayableTileElement>>.Raise(new ReturnToPoolEvent<BasePlayableTileElement>(ElementType, poolObject));
        }

        public async override UniTask PlayDestroy()
        {
            await base.PlayDestroy();
            EventBus<UpdateLevelObjectiveEvent>.Raise(new UpdateLevelObjectiveEvent(ElementType, 1));

            await Deselect();
            ReturnToPool(this);
            await UniTask.CompletedTask;
        }
    }
}