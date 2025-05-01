using Abstracts;
using Interfaces;

namespace UI.WindowParameters
{
    public class LevelFailedWindowParameters : BaseWindowParameters
    {
        public ILevelData LevelData { get; }
        public LevelFailedWindowParameters(ILevelData levelData)
        {
            LevelData = levelData;
        }
    }
}