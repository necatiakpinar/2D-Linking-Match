using System;
using Interfaces;
using Miscs;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data.Models
{
    [Serializable]
    public struct LevelObjectiveData : ILevelObjectiveData
    {
        [SerializeField] private GameElementType _objectiveType;
        [SerializeField] private int _objectiveAmount;

        public GameElementType ObjectiveType => _objectiveType;

        public int ObjectiveAmount
        {
            get => _objectiveAmount;
            set => _objectiveAmount = value;
        }
    }
}