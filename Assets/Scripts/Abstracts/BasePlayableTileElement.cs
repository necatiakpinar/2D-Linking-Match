using System.Collections.Generic;
using Addressables;
using Cysharp.Threading.Tasks;
using Data.Models;
using DG.Tweening;
using EventBus;
using EventBus.Events;
using Interfaces;
using Miscs;
using UnityEngine;
using UnityEngine.U2D;

namespace Abstracts
{
    public class BasePlayableTileElement : BaseTileElement, IPlayableTileElement, IPoolable<BasePlayableTileElement>
    {
        [SerializeField] private PlayableEntitySetType _setType; // Can be removed later, since it is controlled by addressables
        private Sequence _highlightSequence;

        public PlayableEntitySetType SetType => _setType;

        public async override UniTask Init(ElementModel pElementModel)
        {
            await base.Init(pElementModel);
            EnableSpriteRenderer();
            var spriteAtlas = await AddressablesLoader.LoadAssetAsync<SpriteAtlas>(AddressablesKeys.GetKey(AddressablesKeys.AssetKeys.SA_Set1TileElements));
            if (spriteAtlas == null)
            {
                Debug.LogError($"SpriteAtlas not found for {ElementType}");
                return;
            }

            spriteRenderer.sprite = spriteAtlas.GetSprite(ElementType.ToString());
        }

        public void Select()
        {
            if (_highlightSequence != null && _highlightSequence.IsActive())
            {
                _highlightSequence.Kill();
            }

            _highlightSequence = DOTween.Sequence();

            _highlightSequence.Append(transform.DOScale(Vector3.one * 1.1f, 0.2f).SetEase(Ease.InOutSine))
                .Append(transform.DOScale(Vector3.one * 0.9f, 0.2f).SetEase(Ease.InOutSine))
                .Append(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InOutSine));

            _highlightSequence.SetLoops(-1, LoopType.Restart);
        }

        public void Deselect()
        {
            if (_highlightSequence != null && _highlightSequence.IsActive())
                _highlightSequence.Kill();

            transform.localScale = Vector3.one;
        }

        public async UniTask MoveAlongPath(List<Vector2Int> pathPoints, float duration)
        {
            var tsc = new UniTaskCompletionSource();
            if (pathPoints == null || pathPoints.Count < 2)
            {
                Debug.LogError("Path must contain at least 2 points.");
                await tsc.Task;
            }

            Vector3[] path = new Vector3[pathPoints.Count];
            for (int i = 0; i < pathPoints.Count; i++)
            {
                path[i] = new Vector3(pathPoints[i].x, pathPoints[i].y, 0);
            }

            transform.position = path[0];
            transform.DOPath(path, duration, PathType.CatmullRom)
                .SetEase(Ease.InOutSine)
                .SetOptions(false)
                .OnComplete(() => tsc.TrySetResult());

            await tsc.Task;

        }

        public void OnSpawn()
        {
        }
        public void ReturnToPool(BasePlayableTileElement poolObject)
        {
            tileMono = null;
            EventBus<ReturnToPoolEvent<BasePlayableTileElement>>.Raise(new ReturnToPoolEvent<BasePlayableTileElement>(ElementType, poolObject));
        }
        
        public async override UniTask PlayDestroy()
        {
            ReturnToPool(this);
            await UniTask.CompletedTask;
        }
    }
}