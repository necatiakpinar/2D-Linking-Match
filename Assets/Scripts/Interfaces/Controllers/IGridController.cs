using System.Collections.Generic;

namespace Interfaces.Controllers
{
    public interface IGridController
    {
        ITile[,] Tiles { get; }
        public Dictionary<IVector2Int, ITile> TilesDict { get; set; } 
        void AddEventListeners();
        void RemoveEventListeners();
        void CreateGrid();
        void CalculateTileNeighbours();
        
    }
}