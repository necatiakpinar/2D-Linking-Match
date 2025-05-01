using Cysharp.Threading.Tasks;
using Data.Models;
using DG.Tweening;
using Helpers;
using Interfaces;
using Miscs;
using UnityEngine;
using UnityObjects;

namespace Abstracts
{
    public abstract class BaseTileElement : MonoBehaviour, ITileElement
    {
        [SerializeField] private GameElementType _elementType;
        [SerializeField] protected SpriteRenderer _spriteRenderer;

        protected ITile TileMono;
        protected ElementModel ElementModel;
        public GameElementType ElementType => _elementType;
        public ITransform Transform => new UnityTransform(transform);

        public async virtual UniTask Init(ElementModel pElementModel, ITile tileMono)
        {
            ElementModel = pElementModel;
            TileMono = tileMono;
            await UniTask.CompletedTask;
        }

        public async UniTask SetTile(ITile newTile)
        {
            TileMono = newTile as BaseTileMono;
            if (TileMono == null)
                return;

            transform.parent = ((UnityTransform)TileMono.Transform).TransformRef;
            TileMono.SetTileElement(this);
            transform.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.InOutElastic);
        }

        public async virtual UniTask TryToActivate()
        {
            await UniTask.CompletedTask;
        }

        public async virtual UniTask Activate()
        {
            await UniTask.CompletedTask;
        }

        public async virtual UniTask Select()
        {
            await UniTask.CompletedTask;
        }

        public async virtual UniTask Deselect()
        {
            await UniTask.CompletedTask;
        }

        public async virtual UniTask PlayDestroy()
        {
            TileMono.TileElement = null;
            await UniTask.CompletedTask;
        }

        public void EnableSpriteRenderer()
        {
            if (_spriteRenderer == null)
                return;

            _spriteRenderer.enabled = true;
        }
    }
}