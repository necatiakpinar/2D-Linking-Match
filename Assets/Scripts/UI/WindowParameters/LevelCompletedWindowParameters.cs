using Abstracts;
using Interfaces;

namespace UI.WindowParameters
{
    public class LevelCompletedWindowParameters : BaseWindowParameters
    {
        public ILevelData LevelData { get; }
        public LevelCompletedWindowParameters(ILevelData levelData)
        {
            LevelData = levelData;
        }
    }
}