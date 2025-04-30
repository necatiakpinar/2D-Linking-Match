using System.Collections.Generic;
using Interfaces;

namespace StateMachines.StateParameters
{
    public class DecisionStateParameters : IStateParameters
    {
        public LinkedList<ITile> SelectedTiles { get; }

        public DecisionStateParameters(LinkedList<ITile> selectedTiles)
        {
            SelectedTiles = selectedTiles;
        }
    }
}