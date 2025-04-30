using Cysharp.Threading.Tasks;
using Data.Models;
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

        protected BaseTileMono TileMono;
        protected ElementModel ElementModel;
        public GameElementType ElementType => _elementType;
        public ITransform Transform => new UnityTransform(transform);

        public void OnSpawn()
        {

        }
        
        public async virtual UniTask Init(ElementModel pElementModel)
        {
            ElementModel = pElementModel;
            await UniTask.CompletedTask;
        }

        public async UniTask SetTile(ITile newTile)
        {
            TileMono = newTile as BaseTileMono;
            if (TileMono == null)
            {
                Debug.LogError("TileMono is null.");
                return;
            }
            
            transform.parent = TileMono.transform;
            TileMono.SetTileElement(this);
            //transform.localPosition = Vector3.zero;
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

        public abstract UniTask PlayDestroy();

        public void EnableSpriteRenderer()
        {
            if (_spriteRenderer == null)
                return;

            _spriteRenderer.enabled = true;
        }
    }
}