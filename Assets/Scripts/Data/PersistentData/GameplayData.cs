using System;
using Data.Controllers;

namespace Data.PersistentData
{
    [Serializable]
    public class GameplayData
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