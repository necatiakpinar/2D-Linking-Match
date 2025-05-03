using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data.PersistentData;
using EventBus;
using EventBus.Events;
using Interfaces;
using Interfaces.Controllers;
using Miscs;
using UI.WindowParameters;
using ILogger = Interfaces.ILogger;

namespace Controllers
{
    public class LevelController : ILevelController
    {
        private readonly ILevelContainer _levelContainerData;
        private readonly ILogger _logger;
        private ILevelData _currentLevelData;
        private int _currentLevelIndex;
        private List<ILevelObjectiveData> _levelObjectives;
        private int _currentMoveAmount;
        private bool _levelFinished;
        private GameplayData _gameplayData;
        public int CurrentLevelIndex => _currentLevelIndex;
        public ILevelData CurrentLevelData => _currentLevelData;

        public void AddEventListeners()
        {
            EventBusManager.Subscribe<UpdateLevelObjectiveEvent>(UpdateLevelObjective);
            EventBusManager.Subscribe<MoveUsedEvent>(MoveUsed);
            EventBusManager.SubscribeWithResult<GetCurrentLevelDataEvent, ILevelData>(GetCurrentLevelData);
            EventBusManager.SubscribeWithResult<CheckForLevelEndedEvent, UniTask<bool>>(CheckForLevelEnded);
        }

        public void RemoveEventListeners()
        {
            EventBusManager.Unsubscribe<UpdateLevelObjectiveEvent>(UpdateLevelObjective);
            EventBusManager.Unsubscribe<MoveUsedEvent>(MoveUsed);
            EventBusManager.UnsubscribeWithResult<GetCurrentLevelDataEvent, ILevelData>(GetCurrentLevelData);
            EventBusManager.UnsubscribeWithResult<CheckForLevelEndedEvent, UniTask<bool>>(CheckForLevelEnded);
        }

        public LevelController(ILevelContainer levelContainerData, ILogger logger)
        {
            _levelContainerData = levelContainerData;
            _logger = logger;
            _gameplayData = EventBusManager.RaiseWithResult<GetPersistentDataEvent, GameplayData>(new GetPersistentDataEvent());
            AddEventListeners();
            LoadGameplayLevel();
        }

        public void LoadGameplayLevel()
        {
            _currentLevelIndex = _gameplayData.LevelDataController.CurrentLevelIndex;

            if (_currentLevelIndex >= _levelContainerData.Levels.Count)
            {
                _gameplayData.LevelDataController.CurrentLevelIndex = 0;
                EventBusManager.Raise(new SaveDataEvent());
                _currentLevelIndex = 0;
            }

            _currentLevelData = _levelContainerData.Levels[_currentLevelIndex];
            _currentMoveAmount = _currentLevelData.MoveAmount;
            _levelObjectives = new List<ILevelObjectiveData>(_currentLevelData.LevelObjectives);

            _logger.Log($"Current level index: {_currentLevelData.GridSize.x} x {_currentLevelData.GridSize.y}");
        }

        public async void UpdateLevelObjective(UpdateLevelObjectiveEvent @event)
        {
            if (_levelObjectives == null)
            {
                _logger.LogError("Level objectives are null");
                return;
            }

            if (_levelObjectives.Count == 0)
                return;

            foreach (var objective in _levelObjectives)
            {
                if (objective.ObjectiveType == @event.ObjectiveType)
                {
                    objective.ObjectiveAmount -= @event.Amount;
                    if (objective.ObjectiveAmount <= 0)
                    {
                        objective.ObjectiveAmount = 0;
                        _levelObjectives.Remove(objective);
                    }

                    var levelObjectiveUpdatedUI = new UpdateLevelObjectiveUIEvent(objective.ObjectiveType, objective.ObjectiveAmount);
                    EventBusManager.Raise(levelObjectiveUpdatedUI);
                    break;
                }
            }
        }

        public async UniTask<bool> CheckForLevelEnded(CheckForLevelEndedEvent @event)
        {
            if (_levelObjectives.Count == 0 && _currentMoveAmount > 0)
            {
                var gameplayData = EventBusManager.RaiseWithResult<GetPersistentDataEvent, GameplayData>(new GetPersistentDataEvent());
                gameplayData.LevelDataController.IncreaseCurrentLevelIndex();

                EventBusManager.Raise(new SaveDataEvent());
                var levelCompletedParameters = new LevelCompletedWindowParameters(_currentLevelData);
                await EventBusManager.RaiseWithResult<ShowWindowEvent, UniTask>(new ShowWindowEvent(WindowType.LevelCompletedWindow, levelCompletedParameters));
                return true;
            }

            if (_currentMoveAmount <= 0)
            {
                var levelFailedParameters = new LevelFailedWindowParameters(_currentLevelData);
                await EventBusManager.RaiseWithResult<ShowWindowEvent, UniTask>(new ShowWindowEvent(WindowType.LevelFailedWindow, levelFailedParameters));
                return true;
            }

            return false;
        }

        public void MoveUsed(MoveUsedEvent @event)
        {
            _currentMoveAmount--;

            if (_currentMoveAmount <= 0)
                _currentMoveAmount = 0;

            EventBusManager.Raise(new MoveUsedUIEvent(_currentMoveAmount));
        }

        public ILevelData GetCurrentLevelData(GetCurrentLevelDataEvent @event)
        {
            return _currentLevelData;
        }
    }
}