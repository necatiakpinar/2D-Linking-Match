using System.Collections.Generic;
using Abstracts;
using Cysharp.Threading.Tasks;
using EventBus.Events;
using EventBusSystem;
using Helpers;
using Miscs;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private List<BaseWindow> _windows = new List<BaseWindow>();
        [SerializeField] private List<BaseWindow> _cachedWindows = new List<BaseWindow>();
        private Dictionary<WindowType, BaseWindow> _activeWindows = new Dictionary<WindowType, BaseWindow>();

        private static UIManager _instance;

        private void OnEnable()
        {
            EventBusNew.SubscribeWithResult<ShowWindowEvent, UniTask>(OnShowWindow);
            EventBusNew.SubscribeWithResult<HideWindowEvent, UniTask>(OnHideWindow);
            EventBusNew.SubscribeWithResult<DisposeWindowEvent, UniTask>(OnDisposeWindow);
            EventBusNew.SubscribeWithResult<GetWindowEvent, UniTask<BaseWindow>>(GetWindow<BaseWindow>);
        }

        private void OnDisable()
        {
            EventBusNew.UnsubscribeWithResult<ShowWindowEvent, UniTask>(OnShowWindow);
            EventBusNew.UnsubscribeWithResult<HideWindowEvent, UniTask>(OnHideWindow);
            EventBusNew.UnsubscribeWithResult<DisposeWindowEvent, UniTask>(OnDisposeWindow);
            EventBusNew.UnsubscribeWithResult<GetWindowEvent, UniTask<BaseWindow>>(GetWindow<BaseWindow>);
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCachedWindows();
        }

        private void LoadCachedWindows()
        {
            foreach (var cachedWindow in _cachedWindows)
            {
                if (_activeWindows.TryGetValue(cachedWindow.WindowType, out _))
                {
                    LoggerUtil.LogWarning($"Window {cachedWindow.WindowType} is already active!");
                    continue;
                }

                var windowType = cachedWindow.WindowType;
                _activeWindows[windowType] = cachedWindow;
                cachedWindow.gameObject.SetActive(false);
            }
        }

        private async UniTask OnShowWindow(ShowWindowEvent showWindowEvent)
        {
            var window = await GetWindow<BaseWindow>(new GetWindowEvent(showWindowEvent.WindowType, showWindowEvent.Parameters, showWindowEvent.CanUpdate));
            if (window == null)
                return;

            await window.Show();
        }

        private async UniTask OnHideWindow(HideWindowEvent hideWindowEvent)
        {
            if (_activeWindows.TryGetValue(hideWindowEvent.WindowType, out var existingWindow))
                await existingWindow.Hide();
            else
                LoggerUtil.LogWarning($"Window {hideWindowEvent.WindowType} is not active!");
        }

        private async UniTask OnDisposeWindow(DisposeWindowEvent disposeWindowEvent)
        {
            if (_activeWindows.TryGetValue(disposeWindowEvent.WindowType, out var existingWindow))
            {
                if (existingWindow.IsCached)
                    await existingWindow.Hide();
                else
                {
                    existingWindow.Dispose();
                    _activeWindows.Remove(disposeWindowEvent.WindowType);
                }
            }
        }

        private async UniTask<T> GetWindow<T>(GetWindowEvent @event) where T : BaseWindow
        {
            if (_activeWindows.TryGetValue(@event.WindowType, out var existingWindow))
            {
                if (!existingWindow.IsInitialized || @event.CanUpdate)
                    await existingWindow.Init(@event.Parameters);
                else
                    LoggerUtil.LogWarning($"Window {typeof(T).Name} is still initializing!");

                return (T)existingWindow;
            }

            var prefab = _windows.Find(w => w.WindowType == @event.WindowType);
            if (prefab == null)
            {
                LoggerUtil.LogError($"Window prefab for type {@event.WindowType} not found!");
                return null;
            }

            var newWindow = Instantiate(prefab, transform);
            _activeWindows[@event.WindowType] = newWindow;

            await newWindow.Init(@event.Parameters);
            return (T)newWindow;
        }

        public bool IsWindowActive(WindowType windowType)
        {
            return _activeWindows.ContainsKey(windowType);
        }

        private BaseWindow FindPrefab(WindowType windowType)
        {
            return _windows.Find(w => w.WindowType == windowType);
        }
    }
}