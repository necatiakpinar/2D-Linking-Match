using System.Collections.Generic;
using Data.ScriptableObjects.Level;
using Interfaces;
using UnityEngine;

namespace Data.ScriptableObjects.Containers
{
    [CreateAssetMenu(fileName = "SO_LevelContainer", menuName = "Data/ScriptableObjects/Containers/LevelContainer")]
    public class LevelContainerSo : ScriptableObject, ILevelContainer
    {
        [SerializeField] private List<LevelDataSo> _levels = new List<LevelDataSo>();
        public List<ILevelData> Levels => new List<ILevelData>(_levels);
    }
}