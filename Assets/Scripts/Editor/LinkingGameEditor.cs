using Controllers;
using Data.Controllers;
using Data.PersistentData;
using Factories;
using Helpers;
using Loggers;
using Miscs;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class LinkingGameEditor : EditorWindow
    {
        private int _enteredLevelIndex;

        private UnityLogger _logger;
        private UnityObjectFactory _objectFactory;
        private CryptoHelper _cryptoHelper;
        private JsonHelper _jsonHelper;

        private SaveSystemController<GameplayData> _gameplaySaveSystemController;
        private PersistentDataController _persistentDataController;

        [MenuItem("Tools/LinkingGame")]
        public static void ShowWindow()
        {
            GetWindow<LinkingGameEditor>("LinkingGame");
        }

        private void OnEnable()
        {
            _logger = new UnityLogger();
            _cryptoHelper = new CryptoHelper();
            _jsonHelper = new JsonHelper();
            _gameplaySaveSystemController = new SaveSystemController<GameplayData>(Constants.GameplayDataPath, _jsonHelper, _cryptoHelper, _logger);
            _gameplaySaveSystemController.LoadSaveDataFromDisk();
            _persistentDataController = new PersistentDataController((GameplayData)_gameplaySaveSystemController.PersistentData);
        }

        private void OnGUI()
        {
            GUILayout.Label("Linking Game Tools", EditorStyles.boldLabel);

            if (GUILayout.Button("Reset Level Data"))
                _persistentDataController.GameplayData.LevelDataController.ResetCurrentLevelIndex();

            if (GUILayout.Button("Clear All Data"))
                _gameplaySaveSystemController.ClearAllData();

            GUILayout.BeginVertical();
            GUILayout.Label("Select Start Level Index", EditorStyles.boldLabel);
            _enteredLevelIndex = EditorGUILayout.IntField(_enteredLevelIndex);

            if (GUILayout.Button("Save Level Index"))
                _persistentDataController.GameplayData.LevelDataController.SetCurrentLevelIndex(_enteredLevelIndex);
            if (GUILayout.Button("Increase Coin"))
                _persistentDataController.GameplayData.CurrencyDataController.IncreaseCurrency(Miscs.CurrencyType.Coin, 1000);
            if (GUILayout.Button("Decrease Coin"))
                _persistentDataController.GameplayData.CurrencyDataController.IncreaseCurrency(Miscs.CurrencyType.Coin, 1000);
            if (GUILayout.Button("Reset Coin"))
                _persistentDataController.GameplayData.CurrencyDataController.DecreaseCurrency(Miscs.CurrencyType.Coin, 0);

            GUILayout.EndVertical();
        }
    }
}