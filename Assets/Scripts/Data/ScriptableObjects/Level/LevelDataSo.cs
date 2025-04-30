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
        [SerializeField] private int _moveAmount;
        [SerializeField] private LevelType _levelType;
        [SerializeField] private List<LevelObjectiveData> _levelObjectives;
        [SerializeField] private int _levelRewardCoin = 100;
        [SerializeField] private UnityEngine.Vector2Int _gridSize;

        public int LevelIndex => _levelIndex;
        public int MoveAmount => _moveAmount;
        public LevelType LevelType => _levelType;
        public List<ILevelObjectiveData> LevelObjectives
        {
            get
            {
                var list = new List<ILevelObjectiveData>(_levelObjectives.Count);
                foreach (var obj in _levelObjectives)
                {
                    list.Add(obj); 
                }
                return list;
            }
        }
        public int LevelRewardCoin => _levelRewardCoin;
        public IVector2Int GridSize => new Vector2IntAdapter(_gridSize.ToDataVector2Int());
        

    }
}