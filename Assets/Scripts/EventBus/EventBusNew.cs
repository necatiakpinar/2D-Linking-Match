using System;
using System.Collections.Generic;
using Interfaces;

namespace EventBusSystem
{
    public static class EventBusNew
    {
        private static readonly Dictionary<Type, Delegate> _actions = new();
        private static readonly Dictionary<Type, Delegate> _funcs = new();

        public static void Raise<T>(T data) where T : IEvent
        {
            Type type = typeof(T);
            if (_actions.TryGetValue(type, out var existing) && existing is Action<T> action)
            {
                action.Invoke(data);
            }
        }

        public static void Subscribe<T>(Action<T> action) where T : IEvent
        {
            Type type = typeof(T);
            if (_actions.TryGetValue(type, out var existing))
                _actions[type] = Delegate.Combine(existing, action);
            else
                _actions[type] = action;
        }

        public static void Unsubscribe<T>(Action<T> action) where T : IEvent
        {
            Type type = typeof(T);
            if (_actions.TryGetValue(type, out var existing))
            {
                var updated = Delegate.Remove(existing, action);
                if (updated is null)
                    _actions.Remove(type);
                else
                    _actions[type] = updated;
            }
        }
        
        public static TResult RaiseWithResult<T, TResult>(T data) where T : IEvent
        {
            Type type = typeof(T);
            if (_funcs.TryGetValue(type, out var existing) && existing is Func<T, TResult> func)
                return func.Invoke(data);

            return default;
        }


        public static void SubscribeWithResult<T, TResult>(Func<T, TResult> func) where T : IEvent
        {
            Type type = typeof(T);
            if (_funcs.TryGetValue(type, out var existing))
                _funcs[type] = Delegate.Combine(existing, func);
            else
                _funcs[type] = func;
        }

        public static void UnsubscribeWithResult<T, TResult>(Func<T, TResult> func) where T : IEvent
        {
            Type type = typeof(T);
            if (_funcs.TryGetValue(type, out var existing))
            {
                var updated = Delegate.Remove(existing, func);
                if (updated is null)
                    _funcs.Remove(type);
                else
                    _funcs[type] = updated;
            }
        }

        public static void ClearAll()
        {
            _actions.Clear();
            _funcs.Clear();
        }
    }
}