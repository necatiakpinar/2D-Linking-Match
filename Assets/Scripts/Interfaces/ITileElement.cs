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
        UniTask Select();
        UniTask Deselect();
        UniTask PlayDestroy();
        public void EnableSpriteRenderer();
    }
}