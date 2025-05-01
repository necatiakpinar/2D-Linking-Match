using Interfaces;
using Miscs;

namespace EventBus.Events
{
    
    public struct LevelStartedEvent : IEvent
    {
        
    }
    
    public struct UpdateLevelObjectiveEvent : IEvent
    {
        public GameElementType ObjectiveType;
        public int Amount;

        public UpdateLevelObjectiveEvent(GameElementType objectiveType, int amount)
        {
            ObjectiveType = objectiveType;
            Amount = amount;
        }
    }
    
    public struct CheckForLevelEndedEvent : IEvent
    {
        
    }

    public struct GetCurrentLevelDataEvent : IEvent
    {
        
    }

    public struct UpdateLevelObjectiveUIEvent : IEvent
    {
        public GameElementType ObjectiveType;
        public int RemainingAmount;

        public UpdateLevelObjectiveUIEvent(GameElementType objectiveType, int remainingAmount)
        {
            ObjectiveType = objectiveType;
            RemainingAmount = remainingAmount;
        }
    }

    public struct MoveUsedEvent : IEvent
    {

    }
    
    public struct MoveUsedUIEvent : IEvent
    {
        public int RemainingMoves;

        public MoveUsedUIEvent(int remainingMoves)
        {
            RemainingMoves = remainingMoves;
        }
    }
}