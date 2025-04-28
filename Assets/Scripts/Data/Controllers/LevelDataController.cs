using System;
using Data.PersistentData;

namespace Data.Controllers
{
    [Serializable]
    public class LevelDataController
    {
        public int CurrentLevelIndex = 0;

        public void IncreaseCurrentLevelIndex()
        {
            CurrentLevelIndex++;
            PersistentDataManager.SaveDataToDisk();
        }

        public void ResetCurrentLevelIndex()
        {
            CurrentLevelIndex = 0;
            PersistentDataManager.SaveDataToDisk();
        }

        public void SetCurrentLevelIndex(int index)
        {
            CurrentLevelIndex = index;
            PersistentDataManager.SaveDataToDisk();
        }
    }
}