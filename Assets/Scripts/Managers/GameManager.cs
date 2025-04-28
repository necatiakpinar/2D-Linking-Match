using Controllers;
using Cysharp.Threading.Tasks;
using Data.ScriptableObjects;
using Data.ScriptableObjects.Attributes;
using Data.ScriptableObjects.Containers;
using Data.ScriptableObjects.Level;
using EventBus.Events;
using UnityEngine;

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


        private async void Start()
        {
            await Init();
        }

        private async UniTask Init()
        {
            _levelController = new LevelController(_levelContainerSo);
        }


    }
}