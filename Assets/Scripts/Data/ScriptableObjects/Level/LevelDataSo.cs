using System;
using System.Collections.Generic;
using Adapters;
using Data.Models;
using Extensions;
using Interfaces;
using Miscs;
using UnityEngine;

namespace Data.ScriptableObjects.Level
{
    [Serializable]
    [CreateAssetMenu(fileName = "SO_LevelData_000", menuName = "Data/ScriptableObjects/LevelData")]
    public class LevelDataSo : ScriptableObject, ILevelData
    {
        [SerializeField] private int _levelIndex;
        [SerializeField] private LevelType _levelType;
        [SerializeField] private List<LevelObjectiveData> _levelObjectives;
        [SerializeField] private int _levelRewardCoin = 100;
        [SerializeField] private UnityEngine.Vector2Int _gridSize;
        
        public int LevelIndex => _levelIndex;
        public LevelType LevelType => _levelType;
        public List<LevelObjectiveData> LevelObjectives => _levelObjectives;
        public int LevelRewardCoin => _levelRewardCoin;
        public IVector2Int GridSize => new Vector2IntAdapter(_gridSize.ToDataVector2Int());
    }
}