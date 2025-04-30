using Interfaces;

namespace EventBus.Events
{
    public struct TilePressedEvent : IEvent
    {
        public readonly ITile FirstAddedTile;

        public TilePressedEvent(ITile firstAddedTile)
        {
            FirstAddedTile = firstAddedTile;
        }
    }
    
    public struct TryToAddTileEvent : IEvent
    {
        public readonly ITile PossibleMatchTile;

        public TryToAddTileEvent(ITile possibleMatchTile)
        {
            PossibleMatchTile = possibleMatchTile;
        }
    }

    public struct TileReleasedEvent : IEvent
    {
        public ITile TileMono;
        public TileReleasedEvent(ITile tileMono)
        {
            TileMono = tileMono;
        }
    }

    public struct HasAnyTileSelected : IEvent
    {

    }
}