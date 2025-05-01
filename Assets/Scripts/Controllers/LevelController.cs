using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Data.PersistentData;
using EventBus;
using EventBus.Events;
using Interfaces;
using Interfaces.Controllers;
using Miscs;
using UI.WindowParameters;

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
        private EventBinding<UpdateLevelObjectiveEvent> _updateLevelObjectiveEvent;
        private EventBinding<MoveUsedEvent> _moveUsedEvent;
        private EventBinding<GetCurrentLevelDataEvent, ILevelData> _getCurrentLevelDataEvent;
        private EventBinding<CheckForLevelEndedEvent, UniTask<bool>> _checkForLevelFinishedEvent;

        public int CurrentLevelIndex => _currentLevelIndex;
        public ILevelData CurrentLevelData => _currentLevelData;

        public void AddEventListeners()
        {
            _updateLevelObjectiveEvent = new EventBinding<UpdateLevelObjectiveEvent>(UpdateLevelObjective);
            EventBus<UpdateLevelObjectiveEvent>.Register(_updateLevelObjectiveEvent);

            _moveUsedEvent = new EventBinding<MoveUsedEvent>(MoveUsed);
            EventBus<MoveUsedEvent>.Register(_moveUsedEvent);

            _getCurrentLevelDataEvent = new EventBinding<GetCurrentLevelDataEvent, ILevelData>(GetCurrentLevelData);
            EventBus<GetCurrentLevelDataEvent, ILevelData>.Register(_getCurrentLevelDataEvent);

            _checkForLevelFinishedEvent = new EventBinding<CheckForLevelEndedEvent, UniTask<bool>>(CheckForLevelEnded);
            EventBus<CheckForLevelEndedEvent, UniTask<bool>>.Register(_checkForLevelFinishedEvent);
        }

        public void RemoveEventListeners()
        {
            EventBus<UpdateLevelObjectiveEvent>.Deregister(_updateLevelObjectiveEvent);
            EventBus<MoveUsedEvent>.Deregister(_moveUsedEvent);
            EventBus<GetCurrentLevelDataEvent, ILevelData>.Deregister(_getCurrentLevelDataEvent);
            EventBus<CheckForLevelEndedEvent, UniTask<bool>>.Deregister(_checkForLevelFinishedEvent);
        }

        public LevelController(ILevelContainer levelContainerData, ILogger logger)
        {
            _levelContainerData = levelContainerData;
            _logger = logger;
            AddEventListeners();
            LoadGameplayLevel();
        }

        public void LoadGameplayLevel()
        {
            _currentLevelIndex = PersistentDataManager.GameplayData.LevelDataController.CurrentLevelIndex;

            if (_currentLevelIndex >= _levelContainerData.Levels.Count)
            {
                PersistentDataManager.GameplayData.LevelDataController.CurrentLevelIndex = 0;
                PersistentDataManager.SaveDataToDisk();
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
                    EventBus<UpdateLevelObjectiveUIEvent>.Raise(levelObjectiveUpdatedUI);
                    break;
                }
            }
        }

        public async UniTask<bool> CheckForLevelEnded(CheckForLevelEndedEvent @event)
        {
            if (_levelObjectives.Count == 0 && _currentMoveAmount > 0)
            {
                PersistentDataManager.GameplayData.LevelDataController.IncreaseCurrentLevelIndex();
                PersistentDataManager.SaveDataToDisk();
                var levelCompletedParameters = new LevelCompletedWindowParameters(_currentLevelData);
                await EventBus<ShowWindowEvent, UniTask>.Raise(new ShowWindowEvent(WindowType.LevelCompletedWindow, levelCompletedParameters));
                return true;
            }

            if (_currentMoveAmount <= 0)
            {
                var levelFailedParameters = new LevelFailedWindowParameters(_currentLevelData);
                await EventBus<ShowWindowEvent, UniTask>.Raise(new ShowWindowEvent(WindowType.LevelFailedWindow, levelFailedParameters));
                return true;
            }

            return false;
        }

        public void MoveUsed(MoveUsedEvent @event)
        {
            _currentMoveAmount--;

            if (_currentMoveAmount <= 0)
                _currentMoveAmount = 0;
            
            EventBus<MoveUsedUIEvent>.Raise(new MoveUsedUIEvent(_currentMoveAmount));
        }

        public ILevelData GetCurrentLevelData(GetCurrentLevelDataEvent @event)
        {
            return _currentLevelData;
        }
    }
}