using System.Collections.Generic;
using Data.Models;
using Miscs;
using UnityEngine;

namespace Interfaces
{
    public interface ILevelData
    {
        int LevelIndex { get; }
        LevelType LevelType { get; }
        List<LevelObjectiveData> LevelObjectives { get; }
        int LevelRewardCoin { get; }
        Vector2Int GridSize { get; }
    }
}