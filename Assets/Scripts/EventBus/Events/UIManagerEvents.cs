using Abstracts;
using Interfaces;
using Miscs;

namespace EventBus.Events
{
    public struct ShowWindowEvent : IEvent
    {
        public WindowType WindowType;
        public BaseWindowParameters Parameters;
        public bool CanUpdate;

        public ShowWindowEvent(WindowType windowType, BaseWindowParameters parameters = null, bool canUpdate = false)
        {
            WindowType = windowType;
            Parameters = parameters;
            CanUpdate = canUpdate;
        }
    }

    public struct GetWindowEvent : IEvent
    {
        public WindowType WindowType;
        public BaseWindowParameters Parameters;
        public bool CanUpdate;

        public GetWindowEvent(WindowType windowType, BaseWindowParameters parameters = null, bool canUpdate = false)
        {
            WindowType = windowType;
            Parameters = parameters;
            CanUpdate = canUpdate;
        }
    }

    public struct HideWindowEvent : IEvent
    {
        public WindowType WindowType;

        public HideWindowEvent(WindowType windowType)
        {
            WindowType = windowType;
        }
    }

    public struct DisposeWindowEvent : IEvent
    {
        public WindowType WindowType;

        public DisposeWindowEvent(WindowType windowType)
        {
            WindowType = windowType;
        }
    }
}