using Interfaces;

namespace EventBus.Events
{
    public struct StateMachineStateChangedEvent : IEvent
    {
        public string StateName;

        public StateMachineStateChangedEvent(string stateName)
        {
            StateName = stateName;
        }
    }
}