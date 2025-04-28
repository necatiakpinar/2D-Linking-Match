using System;
using Addressables;
using Controllers;
using Cysharp.Threading.Tasks;
using Data.PersistentData;
using Data.ScriptableObjects;
using Data.ScriptableObjects.Attributes;
using Data.ScriptableObjects.Containers;
using Data.ScriptableObjects.Level;
using EventBus.Events;
using Factories;
using Helpers;
using Loggers;
using UnityEngine;
using UnityObjects;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Transform _gridParent;

        private LevelContainerSo _levelContainerSo;
        private TileMonoAttributesSo _tileMonoAttributesSo;
        private GridDataSo _gridDataSo;
        private float _currentRemainingTime;

        private LevelController _levelController;
        private GridController _gridController;
        private InputController _inputController;

        private UnityLogger _logger;
        private UnityObjectFactory _objectFactory;

        private void Awake()
        {
            PersistentDataManager.LoadSaveDataFromDisk();
        }

        private async void Start()
        {
            await Init();
        }

        private async UniTask Init()
        {
            _logger = new UnityLogger();
            _objectFactory = new UnityObjectFactory(_logger);
            await LoadAddressables();
            _levelController = new LevelController(_levelContainerSo, _logger);
            _gridController = new GridController(_gridDataSo, _objectFactory, _levelController.CurrentLevelData, new UnityTransform(_gridParent), _logger);
            _gridController.CreateGrid();
        }

        private async UniTask LoadAddressables()
        {
            _levelContainerSo =
                await AddressablesLoader.LoadAssetAsync<LevelContainerSo>(AddressablesKeys.GetKey(AddressablesKeys.AssetKeys.SO_LevelContainer));
            if (_levelContainerSo == null)
            {
                LoggerUtil.LogError("LevelContainerSo is null");
                return;
            }

            _tileMonoAttributesSo =
                await AddressablesLoader.LoadAssetAsync<TileMonoAttributesSo>(AddressablesKeys.GetKey(AddressablesKeys.AssetKeys.SO_TileMonoAttributes));
            if (_tileMonoAttributesSo == null)
            {
                LoggerUtil.LogError("TileMonoAttributesSo is null");
                return;
            }

            _gridDataSo = await AddressablesLoader.LoadAssetAsync<GridDataSo>(AddressablesKeys.GetKey(AddressablesKeys.AssetKeys.SO_GridData));
            if (_gridDataSo == null)
            {
                LoggerUtil.LogError("GridDataSo is null");
                return;
            }
        }
    }
}