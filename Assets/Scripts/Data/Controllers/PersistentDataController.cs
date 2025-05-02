using Data.PersistentData;
using EventBus;
using EventBus.Events;

namespace Data.Controllers
{
    public class PersistentDataController
    {
        private readonly GameplayData _gameplayData;
        private EventBinding<GetPersistentDataEvent, GameplayData> _getPersistentDataBinding;

        public PersistentDataController(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;
            AddEventBindings();
        }

        private void AddEventBindings()
        {
            _getPersistentDataBinding = new EventBinding<GetPersistentDataEvent, GameplayData>(GetPersistentData);
            EventBus<GetPersistentDataEvent, GameplayData>.Register(_getPersistentDataBinding);
        }

        public void RemoveEventBindings()
        {
            EventBus<GetPersistentDataEvent, GameplayData>.Deregister(_getPersistentDataBinding);
        }

        private GameplayData GetPersistentData(GetPersistentDataEvent @event)
        {
            return _gameplayData;
        }
    }
}