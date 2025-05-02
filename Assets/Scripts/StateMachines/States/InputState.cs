using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events;
using Interfaces;
using StateMachines.StateParameters;
using ILogger = Interfaces.ILogger;

namespace StateMachines.States
{
    public class InputState : IState
    {
        private LinkedList<ITile> _selectedTiles = new LinkedList<ITile>();
        private ITile _latestAddedTile;
        private EventBinding<TilePressedEvent> _tilePressedEventBinding;
        private EventBinding<TileReleasedEvent> _tileReleasedEventBinding;
        private EventBinding<HasAnyTileSelected, bool> _hasAnyTileSelectedEventBinding;
        private EventBinding<TryToAddTileEvent> _tryToAddTileEventBinding;

        public Func<Type, IStateParameters, UniTask> ChangeState { get; set; }

        private readonly ILogger _logger;
        private readonly int _minTilesToCreateMatch = 3;

        public InputState(ILogger logger)
        {
            _logger = logger;
        }

        public void AddEventBindings()
        {
            _tilePressedEventBinding = new EventBinding<TilePressedEvent>(OnTilePressed);
            EventBus<TilePressedEvent>.Register(_tilePressedEventBinding);

            _tileReleasedEventBinding = new EventBinding<TileReleasedEvent>(OnTileReleased);
            EventBus<TileReleasedEvent>.Register(_tileReleasedEventBinding);

            _hasAnyTileSelectedEventBinding = new EventBinding<HasAnyTileSelected, bool>(OnHasAnyTileSelected);
            EventBus<HasAnyTileSelected, bool>.Register(_hasAnyTileSelectedEventBinding);

            _tryToAddTileEventBinding = new EventBinding<TryToAddTileEvent>(OnTryToAddTile);
            EventBus<TryToAddTileEvent>.Register(_tryToAddTileEventBinding);
        }

        public void RemoveEventBindings()
        {
            EventBus<TilePressedEvent>.Deregister(_tilePressedEventBinding);
            EventBus<TileReleasedEvent>.Deregister(_tileReleasedEventBinding);
            EventBus<HasAnyTileSelected, bool>.Deregister(_hasAnyTileSelectedEventBinding);
            EventBus<TryToAddTileEvent>.Deregister(_tryToAddTileEventBinding);
        }

        public async UniTask Enter(IStateParameters parameters = null)
        {
            _logger.Log("InputState.Enter");
            var isLevelEnded = await CheckForLevelEnded();
            if (isLevelEnded)
            {
                await ChangeState.Invoke(typeof(LevelEndState), null);
                return;
            }

            await CheckForAnyLinkAvailable();
            AddEventBindings();
            _latestAddedTile = null;
            _selectedTiles.Clear();
        }

        private async UniTask CheckForAnyLinkAvailable()
        {
            await EventBus<TryToCheckAnyLinkExistEvent, UniTask>.Raise(new TryToCheckAnyLinkExistEvent())[0];
            await UniTask.CompletedTask;
        }

        private async UniTask<bool> CheckForLevelEnded()
        {
            var levelFinished = await EventBus<CheckForLevelEndedEvent, UniTask<bool>>.Raise(new CheckForLevelEndedEvent())[0];
            return levelFinished;
        }

        private void OnTilePressed(TilePressedEvent @event)
        {
            if (@event.FirstAddedTile == null || @event.FirstAddedTile.TileElement == null)
                return;

            _latestAddedTile = @event.FirstAddedTile;
            _latestAddedTile.SelectTile();
            _selectedTiles = new LinkedList<ITile>();
            _selectedTiles.AddLast(_latestAddedTile);
        }

        private async void OnTileReleased(TileReleasedEvent @event)
        {
            if (_selectedTiles.Count >= _minTilesToCreateMatch)
            {
                var decisionStateParameters = new DecisionStateParameters(_selectedTiles);
                await ChangeState.Invoke(typeof(DecisionState), decisionStateParameters);
            }
            else
                DeselectTiles();

            _selectedTiles.Clear();
            _latestAddedTile = null;

        }
        private async void DeselectTiles()
        {
            var selectedTileNode = _selectedTiles.First;
            while (selectedTileNode != null)
            {
                var tile = selectedTileNode.Value;
                if (tile == null)
                    _logger.Log("Tile is null.");
                else
                    tile.DeselectTile();

                selectedTileNode = selectedTileNode.Next;
            }

            await UniTask.CompletedTask;
        }

        private void OnTryToAddTile(TryToAddTileEvent @event)
        {
            if (_latestAddedTile == null || _latestAddedTile.TileElement == null || @event.PossibleMatchTile.TileElement == null)
                return;

            if (_latestAddedTile.TileElement.ElementType != @event.PossibleMatchTile.TileElement.ElementType)
                return;

            if (_selectedTiles.Contains(@event.PossibleMatchTile))
            {
                var possibleMatchTileNode = _selectedTiles.Find(@event.PossibleMatchTile);
                if (possibleMatchTileNode != null && possibleMatchTileNode.Next != null && possibleMatchTileNode.Next.Value == _latestAddedTile)
                {
                    _latestAddedTile.DeselectTile();
                    _selectedTiles.RemoveLast();
                    _latestAddedTile = possibleMatchTileNode.Value;
                }

                return;
            }

            if (!_latestAddedTile.HasTileInNeighbours(@event.PossibleMatchTile))
                return;

            _latestAddedTile = @event.PossibleMatchTile;
            _latestAddedTile.SelectTile();
            _selectedTiles.AddLast(_latestAddedTile);
        }

        private bool OnHasAnyTileSelected(HasAnyTileSelected @event)
        {
            return _selectedTiles.Count > 0;
        }

        public async UniTask Exit()
        {
            RemoveEventBindings();
            await UniTask.CompletedTask;
        }
    }
}