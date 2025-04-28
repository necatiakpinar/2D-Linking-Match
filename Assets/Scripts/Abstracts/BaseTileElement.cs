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
        [SerializeField] protected SpriteRenderer spriteRenderer;

        protected BaseTileMono tileMono;
        protected ElementModel elementModel;
        public GameElementType ElementType => _elementType;
        public ITransform Transform => new UnityTransform(transform);
        public BaseTileMono TileMono => tileMono;

        public void OnSpawn()
        {

        }

        public abstract UniTask PlayDestroy();

        public async virtual UniTask Init(ElementModel pElementModel)
        {
            elementModel = pElementModel;
        }

        public void EnableSpriteRenderer()
        {
            if (spriteRenderer == null)
                return;

            spriteRenderer.enabled = true;
        }
    }
}