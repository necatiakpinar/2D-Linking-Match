using Data;

namespace Interfaces
{
    public interface ITransform
    {
        IVector3 Position { get; set; } 
        IVector3 LocalPosition { get; set; } 
        void SetParent(ITransform parent); 
    }
}