using Abstracts;
using Interfaces;
using Miscs;

namespace EventBus.Events
{
    public struct AddTileElementEvent : IEvent
    {
        public BaseTileElement TileElement;

        public AddTileElementEvent(BaseTileElement tileElement)
        {
            TileElement = tileElement;
        }
    }

    public struct RemoveTileElementEvent : IEvent
    {
        public BaseTileElement TileElement;

        public RemoveTileElementEvent(BaseTileElement tileElement)
        {
            TileElement = tileElement;
        }
    }

    public struct GetTileElementsByTypeEvent : IEvent
    {
        public GameElementType ElementType;
        
        public GetTileElementsByTypeEvent(GameElementType elementType)
        {
            ElementType = elementType;
        }
    }
    
    public struct TryToCheckAnyLinkExistEvent : IEvent
    {
    }
    
}