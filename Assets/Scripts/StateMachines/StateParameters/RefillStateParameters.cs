using System.Collections.Generic;
using Interfaces;

namespace StateMachines.StateParameters
{
    public class RefillStateParameters : IStateParameters
    {
        public LinkedList<ITile> ActivatedTiles { get; }

        public RefillStateParameters(LinkedList<ITile> activatedTiles)
        {
            ActivatedTiles = activatedTiles;
            
        }
    }
}