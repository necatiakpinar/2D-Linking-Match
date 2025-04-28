using Interfaces;
using Miscs;

namespace EventBus.Events
{
    public struct GetCurrentLevelDataEvent : IEvent
    {
    }

    public struct GetGameplayTimerEvent : IEvent
    {
        public float RemainingTime;

        public GetGameplayTimerEvent(float remainingTime)
        {
            RemainingTime = remainingTime;
        }
    }

    public struct GetTilesEvent : IEvent
    {
    }
}
