using Addressables;
using Controllers;
using Cysharp.Threading.Tasks;
using Data.Controllers;
using Data.PersistentData;
using Data.ScriptableObjects;
using Data.ScriptableObjects.Attributes;
using Data.ScriptableObjects.Containers;
using Factories;
using Helpers;
using Interfaces;
using Loggers;
using Miscs;
using StateMachines.States;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private SaveSystemController<GameplayData> _gameplaySaveSystemController;
        private PersistentDataController _persistentDataController;
        private LevelController _levelController;
        private GridController _gridController;
        private UnityLogger _logger;
        private UnityObjectFactory _objectFactory;
        private CryptoHelper _cryptoHelper;
        private JsonHelper _jsonHelper;

        private IStateMachine _stateMachine;
        private IState _levelStartState;
        private IState _inputState;
        private IState _decisionState;
        private IState _refillState;
        private IState _levelEndState;

        private void OnDestroy()
        {
            CleanupEvents();
        }

        private void Awake()
        {
            _logger = new UnityLogger();
            _cryptoHelper = new CryptoHelper();
            _jsonHelper = new JsonHelper();
            _gameplaySaveSystemController = new SaveSystemController<GameplayData>(Constants.GameplayDataPath,_jsonHelper, _cryptoHelper, _logger);
            _gameplaySaveSystemController.LoadSaveDataFromDisk();
            _persistentDataController = new PersistentDataController((GameplayData)_gameplaySaveSystemController.PersistentData);
        }

        private async void Start()
        {
            await Init();
        }

        private async UniTask Init()
        {
            await LoadAddressables();
            
            _objectFactory = new UnityObjectFactory(_logger);
            _levelController = new LevelController(_levelContainerSo, _logger);
            _gridController = new GridController(_gridDataSo, _objectFactory, _levelController.CurrentLevelData, new UnityTransform(_gridParent), _logger);

            _stateMachine = new StateMachines.StateMachine(_logger);
            _levelStartState = new LevelStartState(_gridController, _logger);
            _inputState = new InputState(_logger);
            _decisionState = new DecisionState(_logger);
            _refillState = new RefillState(_logger);
            _levelEndState = new LevelEndState(_logger);

            _stateMachine.AddState(_levelStartState);
            _stateMachine.AddState(_inputState);
            _stateMachine.AddState(_decisionState);
            _stateMachine.AddState(_refillState);
            _stateMachine.AddState(_levelEndState);

            await _stateMachine.ChangeState<LevelStartState>(null);
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
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        private void CleanupEvents()
        {
            if (_levelController != null)
                _levelController.RemoveEventListeners();

            if (_gridController != null)
                _gridController.RemoveEventListeners();

            if (_levelStartState != null)
                _levelStartState.RemoveEventBindings();

            if (_inputState != null)
                _inputState.RemoveEventBindings();

            if (_gameplaySaveSystemController != null)
                _gameplaySaveSystemController.RemoveEventBindings();

            if (_persistentDataController != null)
                _persistentDataController.RemoveEventBindings();
        }
    }
}