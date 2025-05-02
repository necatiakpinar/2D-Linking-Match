using System;
using Data.Controllers;
using Interfaces;

namespace Data.PersistentData
{
    [Serializable]
    public class GameplayData : IGameplayData
    {
        public LevelDataController LevelDataController;
        public CurrencyDataController CurrencyDataController;
        
        public GameplayData()
        {
            LevelDataController = new LevelDataController();
            CurrencyDataController = new CurrencyDataController();
        }
        
    }
}