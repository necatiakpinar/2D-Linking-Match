// using Data.PersistentData;
// using UnityEditor;
// using UnityEngine;
//
// namespace Editor
// {
//     public class LinkingGameEditor : EditorWindow
//     {
//         private int _enteredLevelIndex;
//
//         [MenuItem("Tools/LinkingGame")]
//         public static void ShowWindow()
//         {
//             GetWindow<LinkingGameEditor>("LinkingGame");
//             PersistentDataController.LoadSaveDataFromDisk();
//             
//         }
//
//         private void OnGUI()
//         {
//             GUILayout.Label("Linking Game Tools", EditorStyles.boldLabel);
//
//             if (GUILayout.Button("Reset Level Data"))
//                 PersistentDataController.GameplayData.LevelDataController.ResetCurrentLevelIndex();
//
//             if (GUILayout.Button("Clear All Data"))
//                 PersistentDataController.ClearAllData();
//
//             GUILayout.BeginVertical();
//             GUILayout.Label("Select Start Level Index", EditorStyles.boldLabel);
//             _enteredLevelIndex = EditorGUILayout.IntField(_enteredLevelIndex);
//
//             if (GUILayout.Button("Save Level Index"))
//                 PersistentDataController.GameplayData.LevelDataController.SetCurrentLevelIndex(_enteredLevelIndex);
//             if (GUILayout.Button("Increase Coin"))
//                 PersistentDataController.GameplayData.CurrencyDataController.IncreaseCurrency(Miscs.CurrencyType.Coin, 1000);
//             if (GUILayout.Button("Decrease Coin"))
//                 PersistentDataController.GameplayData.CurrencyDataController.IncreaseCurrency(Miscs.CurrencyType.Coin, 1000);
//             if (GUILayout.Button("Reset Coin"))
//                 PersistentDataController.GameplayData.CurrencyDataController.DecreaseCurrency(Miscs.CurrencyType.Coin, 0);
//             
//             GUILayout.EndVertical();
//         }
//     }
// }