using Cysharp.Threading.Tasks;
using Miscs;

namespace Interfaces
{
    public interface ITileElement
    {
        GameElementType ElementType { get; }
        ITransform Transform { get; }
        UniTask SetTile(ITile newTile);
        UniTask TryToActivate();
        UniTask Activate();
        void Select();
        void Deselect();
        UniTask PlayDestroy();
        public void EnableSpriteRenderer();
    }
}