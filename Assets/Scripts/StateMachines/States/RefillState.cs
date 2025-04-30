using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Interfaces;
using StateMachines.StateParameters;

namespace StateMachines.States
{
    public class RefillState : IState
    {
        private RefillStateParameters _refillStateParameters;

        private readonly ILogger _logger;
        public Func<Type, IStateParameters, UniTask> ChangeState { get; set; }

        public void AddEventBindings()
        {
        }

        public void RemoveEventBindings()
        {
        }

        public RefillState(ILogger logger)
        {
            _logger = logger;
        }

        public async UniTask Enter(IStateParameters parameters = null)
        {
            _logger.LogError("RefillState.Enter");
            _refillStateParameters = (RefillStateParameters)parameters;
            if (_refillStateParameters == null)
            {
                _logger.LogError("RefillStateParameters is null.");
                return;
            }

            await TryToRefillActivatedTiles();
            await ChangeState.Invoke(typeof(InputState), null);
        }

        private async UniTask TryToRefillActivatedTiles()
        {
            var sortedActivatedTiles = _refillStateParameters.ActivatedTiles.OrderByDescending(x => x.Transform.Position.y).ToList();

            foreach (var tile in sortedActivatedTiles)
            {
                if (tile == null)
                {
                    _logger.LogError("Tile is null.");
                    continue;
                }

                await tile.TryToRequestTileElement();
            }

            // var activatedTileNode = _refillStateParameters.ActivatedTiles.First;
            //
            // while (activatedTileNode != null)
            // {
            //     var tile = activatedTileNode.Value;
            //     if (tile == null)
            //         _logger.LogError("Tile is null.");
            //     else
            //         await tile.TryToRequestTileElement();
            //
            //     activatedTileNode = activatedTileNode.Next;
            // }

            await UniTask.CompletedTask;
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