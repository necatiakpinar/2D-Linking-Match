using Data.PersistentData;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            PersistentDataManager.LoadSaveDataFromDisk();
        }
    }
}