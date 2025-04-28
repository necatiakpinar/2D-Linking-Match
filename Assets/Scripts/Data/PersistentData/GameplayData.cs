using System;
using Data.Controllers;

namespace Data.PersistentData
{
    [Serializable]
    public class GameplayData
    {
        public LevelDataController LevelDataController;
        
        public GameplayData()
        {
            LevelDataController = new LevelDataController();
        }
        
    }
}