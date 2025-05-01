using System;
using System.IO;
using Helpers;
using Miscs;
using UnityEngine;

namespace Data.PersistentData
{
    public static class PersistentDataManager
    {
        private static GameplayData _gameplayData;
        private static GameplayData _cachedGameplayData;

        public static GameplayData GameplayData => _gameplayData;

        private static readonly string _filePath =
            $"{Application.persistentDataPath}/{Constants.PlayerDataFileName}.{Constants.SaveFileExtensionName}";

        public static void SaveDataToDisk()
        {
            if (_cachedGameplayData != null && _cachedGameplayData.Equals(_gameplayData))
            {
                LoggerUtil.Log("No changes detected. Save operation skipped.");
                return;
            }

            try
            {
                var jsonData = JsonUtility.ToJson(_gameplayData, true);
                var encryptedData = CryptoHelper.Encrypt(jsonData);
                File.WriteAllText(_filePath, encryptedData);
                _cachedGameplayData = CloneGameplayData(_gameplayData);
                LoggerUtil.Log("GameplayData successfully saved and encrypted.");
            }
            catch (Exception e)
            {
                LoggerUtil.LogError($"Failed to save GameplayData: {e.Message}");
            }
        }
        
        public static void LoadSaveDataFromDisk()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    var encryptedData = File.ReadAllText(_filePath);
                    var jsonData = CryptoHelper.Decrypt(encryptedData);
                    _gameplayData = JsonUtility.FromJson<GameplayData>(jsonData);
                    _cachedGameplayData = CloneGameplayData(_gameplayData);
                    LoggerUtil.Log("GameplayData successfully loaded and decrypted.");
                }
                else
                {
                    LoggerUtil.Log("No existing save data found. Creating new data.");
                    _gameplayData = new GameplayData();
                    SaveDataToDisk();
                }
            }
            catch (Exception e)
            {
                LoggerUtil.LogError($"Failed to load GameplayData: {e.Message}");
                _gameplayData = new GameplayData();
            }
        }

        private static GameplayData CloneGameplayData(GameplayData data)
        {
            return JsonUtility.FromJson<GameplayData>(JsonUtility.ToJson(data));
        }
        
        public static void ClearAllData()
        {
            _gameplayData = new GameplayData();
            SaveDataToDisk();
        }
    }
}