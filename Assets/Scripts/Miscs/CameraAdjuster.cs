using Data.ScriptableObjects;
using Data.ScriptableObjects.Level;
using EventBus;
using EventBus.Events;
using UnityEngine;

namespace Miscs
{
    public class CameraAdjuster : MonoBehaviour
    {
        [SerializeField] private float _cameraXOffset = 0.5f;

        private Camera _camera;
        private float _cameraYOffset = 2.0f;
        private Vector2 _gridSize;
        private float _cameraX, _cameraY;

        private readonly float _cameraYMultiplier = 0.19f;
        private readonly float _minFieldOfView = 75;

        async void Start()
        {
            _camera = Camera.main;
            var currentLevel = EventBus<GetCurrentLevelDataEvent, LevelDataSo>.Raise(new GetCurrentLevelDataEvent())[0];
            _gridSize = currentLevel.GridSize;
            _cameraYOffset = _gridSize.y * _cameraYMultiplier * -1;
            _cameraX = (_gridSize.x / 2) - _cameraXOffset;
            _cameraY = (_gridSize.y / 2) + _cameraYOffset;
            transform.position = new Vector3(_cameraX, _cameraY, transform.position.z);
            if (_gridSize.x == 7)
            {
                _camera!.fieldOfView = 88;
            }
            else if (_gridSize.x == 8)
            {
                _camera!.fieldOfView = 92;
            }
            else if (_gridSize.x == 9)
            {
                _camera!.fieldOfView = 96;
            }
            else
                _camera!.fieldOfView = _minFieldOfView;
        }
    }
}