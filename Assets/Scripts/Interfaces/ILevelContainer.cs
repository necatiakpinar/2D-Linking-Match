using System.Collections.Generic;

namespace Interfaces
{
    public interface ILevelContainer
    {
        List<ILevelData> Levels { get; }
    }
}