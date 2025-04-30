using System.Collections.Generic;
using Data.Models;
using Miscs;
using UnityEngine;

namespace Interfaces
{
    public interface ILevelData
    {
        int LevelIndex { get; }
        int MoveAmount { get; }
        LevelType LevelType { get; }
        List<ILevelObjectiveData> LevelObjectives { get; }
        int LevelRewardCoin { get; }
        IVector2Int GridSize { get; }
    }
}