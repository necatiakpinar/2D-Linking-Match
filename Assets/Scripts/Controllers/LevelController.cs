using System.Collections.Generic;
using Data.PersistentData;
using EventBus;
using EventBus.Events;
using Interfaces;
using Interfaces.Controllers;

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
        private EventBinding<UpdateLevelObjectiveEvent> _updateLevelObjectiveEvent;
        private EventBinding<MoveUsedEvent> _moveUsedEvent; 
        private EventBinding<GetCurrentLevelData, ILevelData> _getCurrentLevelDataEvent;
        
        public int CurrentLevelIndex => _currentLevelIndex;
        public ILevelData CurrentLevelData => _currentLevelData;

        public void AddEventListeners()
        {
            _updateLevelObjectiveEvent = new EventBinding<UpdateLevelObjectiveEvent>(UpdateLevelObjective);
            EventBus<UpdateLevelObjectiveEvent>.Register(_updateLevelObjectiveEvent);

            _moveUsedEvent = new EventBinding<MoveUsedEvent>(MoveUsed);
            EventBus<MoveUsedEvent>.Register(_moveUsedEvent);
            
            _getCurrentLevelDataEvent = new EventBinding<GetCurrentLevelData, ILevelData>(GetCurrentLevelData);
            EventBus<GetCurrentLevelData, ILevelData>.Register(_getCurrentLevelDataEvent);
        }

        public void RemoveEventListeners()
        {
            EventBus<UpdateLevelObjectiveEvent>.Deregister(_updateLevelObjectiveEvent);
            EventBus<MoveUsedEvent>.Deregister(_moveUsedEvent);
            EventBus<GetCurrentLevelData, ILevelData>.Deregister(_getCurrentLevelDataEvent);
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

        public void UpdateLevelObjective(UpdateLevelObjectiveEvent @event)
        {
            if (_levelObjectives == null)
            {
                _logger.LogError("Level objectives are null");
                return;
            }

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

            if (_levelObjectives.Count == 0)
            {
                //todo: level finished!
                _logger.LogError("level Completed!");
            }
            
        }
        
        public void MoveUsed(MoveUsedEvent @event)
        {
            _currentMoveAmount--;

            if (_currentMoveAmount <= 0)
            {
                //todo: Fail Level
            }
        }
        
        public ILevelData GetCurrentLevelData(GetCurrentLevelData @event)
        {
            return _currentLevelData;
        }
    }
}