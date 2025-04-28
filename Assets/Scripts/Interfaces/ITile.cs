using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data;
using Miscs;

namespace Interfaces
{
    public interface ITile
    {
        IVector2Int Coordinates { get; }
        ITileElement TileElement { get; }
        ITransform Transform { get; }
        Dictionary<TileDirectionType, ITile> Neighbours { get; }
        UniTask Init(IVector2Int coordinates, ITileElement slotObject);
        void SetNeighbours(Dictionary<TileDirectionType, ITile> neighbours);
    }
}