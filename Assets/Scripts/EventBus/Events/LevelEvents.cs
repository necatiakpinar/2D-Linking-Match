using Interfaces;
using Miscs;

namespace EventBus.Events
{
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

    public struct GetCurrentLevelData : IEvent
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
}