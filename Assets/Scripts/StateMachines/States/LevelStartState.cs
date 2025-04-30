using Cysharp.Threading.Tasks;
using Interfaces;
using System;

namespace StateMachines.States
{
    public class LevelStartState : IState
    {
        public Func<Type, IStateParameters, UniTask> ChangeState { get; set; }

        private readonly IGridController _gridController;
        private readonly ILogger _logger;
        private readonly int _initialWaitDuration = 500;

        public LevelStartState(IGridController gridController, ILogger logger)
        {
            _gridController = gridController;
            _logger = logger;
        }

        public void AddEventBindings()
        {

        }
        public void RemoveEventBindings()
        {

        }

        public async UniTask Enter(IStateParameters parameters = null)
        {
            AddEventBindings();
            _logger.Log("StartingState.Enter");
            _gridController.CreateGrid();
            await UniTask.Delay(_initialWaitDuration);
            await ChangeState.Invoke(typeof(InputState), null);
        }

        public async UniTask Exit()
        {
            RemoveEventBindings();
            await UniTask.CompletedTask;
        }

        public void Update()
        {
        }
    }
}