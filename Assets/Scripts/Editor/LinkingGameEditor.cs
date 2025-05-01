using Data.PersistentData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class LinkingGameEditor : EditorWindow
    {
        private int _enteredLevelIndex;

        [MenuItem("Tools/LinkingGame")]
        public static void ShowWindow()
        {
            GetWindow<LinkingGameEditor>("LinkingGame");
            PersistentDataManager.LoadSaveDataFromDisk();
        }

        private void OnGUI()
        {
            GUILayout.Label("Linking Game Tools", EditorStyles.boldLabel);

            if (GUILayout.Button("Reset Level Data"))
                PersistentDataManager.GameplayData.LevelDataController.ResetCurrentLevelIndex();

            if (GUILayout.Button("Clear All Data"))
                PersistentDataManager.ClearAllData();

            GUILayout.BeginVertical();
            GUILayout.Label("Select Start Level Index", EditorStyles.boldLabel);
            _enteredLevelIndex = EditorGUILayout.IntField(_enteredLevelIndex);

            if (GUILayout.Button("Save Level Index"))
                PersistentDataManager.GameplayData.LevelDataController.SetCurrentLevelIndex(_enteredLevelIndex);
            if (GUILayout.Button("Increase Coin"))
                PersistentDataManager.GameplayData.CurrencyDataController.IncreaseCurrency(Miscs.CurrencyType.Coin, 1000);
            if (GUILayout.Button("Decrease Coin"))
                PersistentDataManager.GameplayData.CurrencyDataController.IncreaseCurrency(Miscs.CurrencyType.Coin, 1000);
            if (GUILayout.Button("Reset Coin"))
                PersistentDataManager.GameplayData.CurrencyDataController.DecreaseCurrency(Miscs.CurrencyType.Coin, 0);
            
            GUILayout.EndVertical();
        }
    }
}