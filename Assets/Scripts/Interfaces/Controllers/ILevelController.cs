using Cysharp.Threading.Tasks;
using EventBus.Events;

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
        ILevelData GetCurrentLevelData(GetCurrentLevelDataEvent @event);
        UniTask<bool> CheckForLevelEnded(CheckForLevelEndedEvent @event);
    }
}