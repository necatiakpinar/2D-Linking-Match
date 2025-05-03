using Data.PersistentData;
using EventBus;
using EventBus.Events;

namespace Data.Controllers
{
    public class PersistentDataController
    {
        private readonly GameplayData _gameplayData;

        public GameplayData GameplayData => _gameplayData;
        public PersistentDataController(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;
            AddEventBindings();
        }

        private void AddEventBindings()
        {
            EventBusManager.SubscribeWithResult<GetPersistentDataEvent, GameplayData>(GetPersistentData);
        }

        public void RemoveEventBindings()
        {
            EventBusManager.UnsubscribeWithResult<GetPersistentDataEvent, GameplayData>(GetPersistentData);
        }

        private GameplayData GetPersistentData(GetPersistentDataEvent @event)
        {
            return _gameplayData;
        }
    }
}