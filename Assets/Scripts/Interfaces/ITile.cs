using Cysharp.Threading.Tasks;
using Data;

namespace Interfaces
{
    public interface ITile
    {
        IVector2Int Coordinates { get; }
        ITileElement TileElement { get; }
        ITransform Transform { get; }
        UniTask Init(IVector2Int coordinates, ITileElement slotObject);
    }
}