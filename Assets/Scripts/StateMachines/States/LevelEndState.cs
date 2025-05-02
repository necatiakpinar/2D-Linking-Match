using System;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events;
using Interfaces;
using Miscs;

namespace StateMachines.States
{
    public class LevelEndState : IState
    {
        private ILogger _logger;
        public Func<Type, IStateParameters, UniTask> ChangeState { get; set; }

        public LevelEndState(ILogger logger)
        {
            _logger = logger;
        }

        public async UniTask Enter(IStateParameters parameters = null)
        {
            _logger.Log("LevelEndState.Enter");
            EventBus<HideWindowEvent, UniTask>.Raise(new HideWindowEvent(WindowType.GameplayWindow));
            await UniTask.CompletedTask;
        }

        public void AddEventBindings()
        {
        }

        public void RemoveEventBindings()
        {
        }
        

        public async UniTask Exit()
        {
            await UniTask.CompletedTask;
        }

    }
}