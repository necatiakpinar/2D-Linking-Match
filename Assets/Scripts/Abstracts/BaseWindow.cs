using System;
using Cysharp.Threading.Tasks;
using Miscs;
using UnityEngine;

namespace Abstracts
{
    public abstract class BaseWindow : MonoBehaviour
    {
        [SerializeField] private WindowType _windowType;
        [SerializeField] private bool _isCached;
        
        private bool _isInitialized;
        private int _currentSortingOrder;
        
        public WindowType WindowType => _windowType;
        public bool IsCached => _isCached;
        public bool IsInitialized => _isInitialized;
        
        public event Action OnShowAction;
        public event Action OnHideAction;
        
        public async UniTask Init(BaseWindowParameters parameters = null)
        {
            await OnInit(parameters);
            _isInitialized = true;
        }
        
        protected abstract UniTask OnInit(BaseWindowParameters parameters = null);
        
        public virtual UniTask Show()
        {
            gameObject.SetActive(true);
            OnShowAction?.Invoke();
            return UniTask.CompletedTask;
        }

        public virtual UniTask Hide()
        {
            _isInitialized = false;
            gameObject.SetActive(false);
            OnHideAction?.Invoke();
            OnHide();
            return UniTask.CompletedTask;
        }

        public abstract UniTask OnHide();
        
        public virtual void Dispose()
        {
            Destroy(gameObject);
        }
    }
}