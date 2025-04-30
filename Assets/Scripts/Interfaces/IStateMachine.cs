using System;
using Cysharp.Threading.Tasks;

namespace Interfaces
{
    public interface IStateMachine
    {
        void AddState<T>(T state) where T : IState;
        UniTask ChangeState<T>(IStateParameters parameters = null) where T : IState;
        UniTask ChangeStateInternal(Type targetType, IStateParameters parameters = null);
        void Update();
    }
}