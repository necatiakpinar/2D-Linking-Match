using Miscs;

namespace Interfaces
{
    public interface ILevelObjectiveData
    {
        GameElementType ObjectiveType { get; }
        int ObjectiveAmount { get; set; }
    }
}