using System;
using System.IO;
using Data.PersistentData;
using EventBus.Events;
using EventBusSystem;
using Interfaces;
using Interfaces.Controllers;
using ILogger = Interfaces.ILogger;

namespace Controllers
{
    public class SaveSystemController<T> : ISaveSystemController where T : IGameplayData
    {
        private IGameplayData _persistentData;
        private IGameplayData _cachedPersistentData;
   
        public IGameplayData PersistentData => _persistentData;
        public IGameplayData CachedPersistentData => _cachedPersistentData;
        public string DataFilePath => _gameplayFilePath;
        public ICryptoHelper CryptoHelper => _cryptoHelper;

        private readonly string _gameplayFilePath;
        private readonly IJsonHelper _jsonHelper;
        private readonly ICryptoHelper _cryptoHelper;
        private readonly ILogger _logger;
        public SaveSystemController(string gameplayFilePath, IJsonHelper jsonHelper, ICryptoHelper cryptoHelper, ILogger logger)
        {
            _gameplayFilePath = gameplayFilePath;
            _jsonHelper = jsonHelper;
            _cryptoHelper = cryptoHelper;
            _logger = logger;

            AddEventBindings();
        }

        public void AddEventBindings()
        {
            EventBusNew.Subscribe<SaveDataEvent>(SaveDataToDisk);
        }

        public void RemoveEventBindings()
        {
            EventBusNew.Unsubscribe<SaveDataEvent>(SaveDataToDisk);
        }

        public void SaveDataToDisk(SaveDataEvent @event)
        {
            if (_cachedPersistentData != null && _cachedPersistentData.Equals(_persistentData))
            {
                _logger.Log("No changes detected. Save operation skipped.");
                return;
            }

            try
            {
                var jsonData = _jsonHelper.ToJson(_persistentData, true);
                var encryptedData = _cryptoHelper.Encrypt(jsonData);
                File.WriteAllText(DataFilePath, encryptedData);
                _cachedPersistentData = CloneGameplayData(_persistentData);
                _logger.Log("GameplayData successfully saved and encrypted.");
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save GameplayData: {e.Message}");
            }
        }

        public void LoadSaveDataFromDisk()
        {
            try
            {
                if (File.Exists(DataFilePath))
                {
                    var encryptedData = File.ReadAllText(DataFilePath);
                    var jsonData = _cryptoHelper.Decrypt(encryptedData);
                    _persistentData = _jsonHelper.FromJson(jsonData, typeof(T)) as IGameplayData;
                    _cachedPersistentData = CloneGameplayData(_persistentData);
                    _logger.Log("GameplayData successfully loaded and decrypted.");
                }
                else
                {
                    _logger.Log("No existing save data found. Creating new data.");
                    _persistentData = new GameplayData();
                    SaveDataToDisk(null);
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to load GameplayData: {e.Message}");
                _persistentData = new GameplayData();
            }
        }

        private IGameplayData CloneGameplayData(IGameplayData data)
        {
            return _jsonHelper.FromJson(_jsonHelper.ToJson(data, false), typeof(T)) as IGameplayData;
        }
        
        public void ClearAllData()
        {
            try
            {
                if (File.Exists(DataFilePath))
                {
                    File.Delete(DataFilePath);
                    _logger.Log("All save data cleared.");
                }
                else
                {
                    _logger.Log("No existing save data found to clear.");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to clear save data: {e.Message}");
            }
        }
    }
}