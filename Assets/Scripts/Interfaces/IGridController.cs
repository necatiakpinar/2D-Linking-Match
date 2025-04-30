using System.Collections.Generic;

namespace Interfaces
{
    public interface IGridController
    {
        ITile[,] Tiles { get; }
        public Dictionary<IVector2Int, ITile> TilesDict { get; set; } 
        void CreateGrid();
        void CalculateTileNeighbours();
    }
}