using System.Collections.Generic;
using Data.PersistentData;
using Data.ScriptableObjects.Containers;
using Data.ScriptableObjects.Level;
using Miscs;

namespace Controllers
{
    public class LevelController
    {
        private LevelContainerSo _levelContainerSo;
        private LevelDataSo _currentLevelDataSo;
        private int _currentLevelIndex;
        private Dictionary<GameElementType, int> _levelObjectives = new Dictionary<GameElementType, int>();

        public LevelDataSo CurrentLevelDataSo => _currentLevelDataSo;
        public Dictionary<GameElementType, int> LevelObjectives => _levelObjectives;

        public LevelController(LevelContainerSo levelContainerSo)
        {
            _levelContainerSo = levelContainerSo;
            LoadGameplayLevel();
        }

        private void LoadGameplayLevel()
        {
            _currentLevelIndex = PersistentDataManager.GameplayData.LevelDataController.CurrentLevelIndex;

            if (_currentLevelIndex >= _levelContainerSo.Levels.Count)
            {
                PersistentDataManager.GameplayData.LevelDataController.CurrentLevelIndex = 0;
                PersistentDataManager.SaveDataToDisk();
                _currentLevelIndex = 0;
            }

            _currentLevelDataSo = _levelContainerSo.Levels[_currentLevelIndex];
        }

    }
}