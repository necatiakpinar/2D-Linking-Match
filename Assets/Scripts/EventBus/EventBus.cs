using System.Collections.Generic;
using Interfaces;

namespace EventBus
{
    public static class EventBus<T> where T : IEvent
    {
        static readonly HashSet<IEventBinding<T>> bindings = new HashSet<IEventBinding<T>>();

        public static void Register(IEventBinding<T> binding) => bindings.Add(binding);
        public static void Deregister(IEventBinding<T> binding) => bindings.Remove(binding);

        public static void Raise(T @event)
        {
            var snapshot = new HashSet<IEventBinding<T>>(bindings);

            foreach (var binding in snapshot)
            {
                if (bindings.Contains(binding))
                {
                    binding.OnEvent?.Invoke(@event);
                    binding.OnEventNoArgs?.Invoke();
                }
            }
        }

        public static void Clear()
        {
            bindings.Clear();
        }
    }

    public static class EventBus<T, TResult> where T : IEvent
    {
        static readonly HashSet<IEventBinding<T, TResult>> bindings = new HashSet<IEventBinding<T, TResult>>();

        public static void Register(EventBinding<T, TResult> binding) => bindings.Add(binding);
        public static void Deregister(EventBinding<T, TResult> binding) => bindings.Remove(binding);

        public static List<TResult> Raise(T @event)
        {
            var results = new List<TResult>();
            var snapshot = new HashSet<IEventBinding<T, TResult>>(bindings);

            foreach (var binding in snapshot)
            {
                if (bindings.Contains(binding))
                {
                    var result = binding.OnEventWithReturn != null ? binding.OnEventWithReturn.Invoke(@event) : default;
                    if (result is not null)
                    {
                        results.Add(result);
                    }
                }
            }

            return results;
        }

        public static void Clear()
        {
            bindings.Clear();
        }
    }
}