using System;
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
            EventBusManager.Raise(new SaveDataEvent());
        }

        public void ResetCurrentLevelIndex()
        {
            CurrentLevelIndex = 0;
            EventBusManager.Raise(new SaveDataEvent());
        }

        public void SetCurrentLevelIndex(int index)
        {
            CurrentLevelIndex = index;
            EventBusManager.Raise(new SaveDataEvent());
        }
    }
}