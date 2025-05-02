using System;
using Data.PersistentData;
using EventBus;
using EventBus.Events;

namespace Data.Controllers
{
    [Serializable]
    public class LevelDataController
    {
        public int CurrentLevelIndex = 0;

        public void IncreaseCurrentLevelIndex()
        {
            CurrentLevelIndex++;
            EventBus<SaveDataEvent>.Raise(new SaveDataEvent());
        }

        public void ResetCurrentLevelIndex()
        {
            CurrentLevelIndex = 0;
            EventBus<SaveDataEvent>.Raise(new SaveDataEvent());
        }

        public void SetCurrentLevelIndex(int index)
        {
            CurrentLevelIndex = index;
            EventBus<SaveDataEvent>.Raise(new SaveDataEvent());
        }
    }
}