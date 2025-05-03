using System.Collections.Generic;
using EventBus;
using EventBus.Events;
using Helpers;
using Interfaces;
using UnityEngine;

namespace Miscs
{
    public class CameraAdjuster : MonoBehaviour
    {
        private Camera _camera;
        private float _cameraYOffset = 2.0f;
        private IVector2Int _gridSize;
        private float _cameraX, _cameraY;

        private readonly float _cameraXOffset = 0.5f;
        private readonly float _cameraYMultiplier = 0.19f;
        private readonly float _minFieldOfView = 75;

        private readonly Dictionary<int, float> _fieldOfViewMap = new()
        {
            { 7, 88f },
            { 8, 92f },
            { 9, 96f },
            { 10, 100f },
        };

        private void OnEnable()
        {
            EventBusManager.Subscribe<LevelStartedEvent>(OnLevelStarted);
        }

        private void OnDisable()
        {
            EventBusManager.Unsubscribe<LevelStartedEvent>(OnLevelStarted);
        }

        void Awake()
        {
            _camera = Camera.main;
        }

        private void OnLevelStarted(LevelStartedEvent @event)
        {
            Init();
        }

        private void Init()
        {
            //var currentLevel = EventBus<GetCurrentLevelDataEvent, ILevelData>.Raise(new GetCurrentLevelDataEvent())[0];
            var currentLevel = EventBusManager.RaiseWithResult<GetCurrentLevelDataEvent, ILevelData>(new GetCurrentLevelDataEvent());
            if (currentLevel == null)
            {
                LoggerUtil.LogError("Current level is null!");
                return;
            }

            _gridSize = currentLevel.GridSize;
            var isEven = _gridSize.x % 2 == 0;
            var gridXHalf = _gridSize.x / 2;
            var gridYHalf = _gridSize.y / 2;
            _cameraX = isEven ? gridXHalf - _cameraXOffset : gridXHalf;
            _cameraY = gridYHalf;
            transform.position = new Vector3(_cameraX, _cameraY, transform.position.z);

            var fieldOfView = _fieldOfViewMap.GetValueOrDefault(_gridSize.x, _minFieldOfView);
            _camera!.fieldOfView = fieldOfView;
        }
    }
}