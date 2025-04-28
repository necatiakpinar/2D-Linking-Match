using System.Collections.Generic;
using Data.ScriptableObjects.Level;
using UnityEngine;

namespace Data.ScriptableObjects.Containers
{
    [CreateAssetMenu(fileName = "LevelContainer", menuName = "Data/ScriptableObjects/Containers/LevelContainer")]
    public class LevelContainerSo : ScriptableObject
    {
        [SerializeField] private List<LevelDataSo> _levels = new List<LevelDataSo>();
        public List<LevelDataSo> Levels => _levels;

    }
}