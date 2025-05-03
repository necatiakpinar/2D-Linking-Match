using System;
using EventBus.Events;
using EventBusSystem;

namespace Data.Controllers
{
    [Serializable]
    public class LevelDataController
    {
        public int CurrentLevelIndex = 0;

        public void IncreaseCurrentLevelIndex()
        {
            CurrentLevelIndex++;
            EventBusNew.Raise(new SaveDataEvent());
        }

        public void ResetCurrentLevelIndex()
        {
            CurrentLevelIndex = 0;
            EventBusNew.Raise(new SaveDataEvent());
        }

        public void SetCurrentLevelIndex(int index)
        {
            CurrentLevelIndex = index;
            EventBusNew.Raise(new SaveDataEvent());
        }
    }
}