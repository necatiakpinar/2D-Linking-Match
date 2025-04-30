using System;
using Cysharp.Threading.Tasks;
using Interfaces;

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
            _logger.LogError("LevelEndState.Enter");
            await UniTask.CompletedTask;
        }
   
        public void AddEventBindings()
        {
        }
        
        public void RemoveEventBindings()
        {
        }

        public void Update()
        {
        }
        
        public async UniTask Exit()
        {
            await UniTask.CompletedTask;
        }
        
    }
}