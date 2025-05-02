using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Miscs;

namespace Interfaces
{
    public interface ITile
    {
        IVector2Int Coordinates { get; }
        ITileElement TileElement { get; set;  }
        ITransform Transform { get; }
        bool IsSpawner { get; }
        Dictionary<TileDirectionType, ITile> Neighbours { get; set; }
        Dictionary<TileDirectionType, ITile> AboveNeighbours { get; set; }
        Dictionary<TileDirectionType, ITile> BelowNeighbours { get; set; }
        void Init(IVector2Int coordinates, ITileElement tileElement, bool isSpawner = false);
        void SetTileElement(ITileElement tileElement);
        bool HasTileInNeighbours(ITile tile);
        void SelectTile();
        void DeselectTile();
        UniTask TryToActivate();
        UniTask Activate();
        UniTask<bool> TryToDropTileElement(ITile requestedTile);
        UniTask<bool> DropTileElement(ITile requestedTile);
        UniTask TryToRequestTileElement();
        UniTask RequestTileElement();
    }
}