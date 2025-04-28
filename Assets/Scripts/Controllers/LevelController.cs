using Data.PersistentData;
using Interfaces;

namespace Controllers
{
    public class LevelController
    {
        private readonly ILevelContainer _levelContainerData;
        private readonly ILogger _logger;
        private ILevelData _currentLevelData;
        private int _currentLevelIndex;

        public ILevelData CurrentLevelData => _currentLevelData;

        public LevelController(ILevelContainer levelContainerData, ILogger logger)
        {
            _levelContainerData = levelContainerData;
            _logger = logger;

            LoadGameplayLevel();
        }

        private void LoadGameplayLevel()
        {
            _currentLevelIndex = PersistentDataManager.GameplayData.LevelDataController.CurrentLevelIndex;

            if (_currentLevelIndex >= _levelContainerData.Levels.Count)
            {
                PersistentDataManager.GameplayData.LevelDataController.CurrentLevelIndex = 0;
                PersistentDataManager.SaveDataToDisk();
                _currentLevelIndex = 0;
            }

            _currentLevelData = _levelContainerData.Levels[_currentLevelIndex];
            _logger.Log($"Current level index: {_currentLevelData.GridSize.x} x {_currentLevelData.GridSize.y}");
        }
    }
}