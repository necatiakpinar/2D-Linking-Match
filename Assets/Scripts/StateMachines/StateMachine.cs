using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events;
using Interfaces;
using ILogger = Interfaces.ILogger;

namespace StateMachines
{
    public class StateMachine : IStateMachine
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _currentState;
        private readonly ILogger _logger;
        
        public StateMachine(ILogger logger)
        {
            _logger = logger;
        }
        
        public void AddState<T>(T state) where T : IState
        {
            var type = state.GetType();
            if (_states.ContainsKey(type))
            {
                _logger.LogError($"{type.Name} already added to StateMachine.");
                return;
            }

            _logger.Log(type);
            state.ChangeState = ChangeStateInternal;
            _states.Add(type, state);
        }

        public async UniTask ChangeState<T>(IStateParameters parameters = null) where T : IState
        {
            await ChangeStateInternal(typeof(T), parameters);
        }

        public async UniTask ChangeStateInternal(Type targetType, IStateParameters parameters = null)
        {
            if (!_states.TryGetValue(targetType, out var newState))
            {
                _logger.Log($"State {targetType.Name} not registered.");
                return;
            }

            if (_currentState != null)
                await _currentState.Exit();

            _currentState = newState;

            if (_currentState != null)
                await _currentState.Enter(parameters);

            EventBus<StateMachineStateChangedEvent>.Raise(new StateMachineStateChangedEvent(targetType.Name));
        }
    }
}