using EventBus.Events;
using Miscs;

namespace Interfaces.Controllers
{
    public interface ILevelController
    {
        int CurrentLevelIndex { get; }
        ILevelData CurrentLevelData { get; }

        void AddEventListeners();
        void RemoveEventListeners();
        void LoadGameplayLevel();
        void UpdateLevelObjective(UpdateLevelObjectiveEvent @event);
        void MoveUsed(MoveUsedEvent @event);
        ILevelData GetCurrentLevelData(GetCurrentLevelData @event);
    }
}