using System;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events;
using Interfaces;
using StateMachines.StateParameters;

namespace StateMachines.States
{
    public class DecisionState : IState
    {
        private DecisionStateParameters _decisionStateParameters;

        private readonly ILogger _logger;
        public Func<Type, IStateParameters, UniTask> ChangeState { get; set; }

        public void AddEventBindings()
        {
        }

        public void RemoveEventBindings()
        {
        }

        public DecisionState(ILogger logger)
        {
            _logger = logger;
        }

        public async UniTask Enter(IStateParameters parameters = null)
        {
            _logger.Log("DecisionState.Enter");
            _decisionStateParameters = (DecisionStateParameters)parameters;
            if (_decisionStateParameters == null)
            {
                _logger.LogError("DecisionStateParameters is null.");
                return;
            }

            await ActivateTiles();
            var refillStateParameters = new RefillStateParameters(_decisionStateParameters.SelectedTiles);
            await ChangeState.Invoke(typeof(RefillState), refillStateParameters);
        }

        private async UniTask ActivateTiles()
        {
            var selectedTileNode = _decisionStateParameters.SelectedTiles.First;

            while (selectedTileNode != null)
            {
                var tile = selectedTileNode.Value;
                if (tile == null)
                    _logger.LogError("Tile is null.");
                else
                    await tile.TryToActivate();

                selectedTileNode = selectedTileNode.Next;
            }

            EventBusManager.Raise(new MoveUsedEvent());
        }

        public async UniTask Exit()
        {
            await UniTask.CompletedTask;
        }
    }
}