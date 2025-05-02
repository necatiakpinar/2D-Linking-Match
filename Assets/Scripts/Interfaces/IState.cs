using System;
using Cysharp.Threading.Tasks;

namespace Interfaces
{
    public interface IState
    {
        void AddEventBindings();
        void RemoveEventBindings();
        public Func<Type,IStateParameters, UniTask> ChangeState { get; set; }
        UniTask Enter(IStateParameters parameters = null);
        UniTask Exit();
    }
}