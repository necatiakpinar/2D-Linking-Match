using Data;

namespace Interfaces
{
    public interface ITransform
    {
        IVector3 Position { get; set; } 
        IVector3 LocalPosition { get; set; } 
        IQuaternion Rotation { get; set; }
        IQuaternion LocalRotation { get; set; }
        IVector3 Scale { get; set; }
        IVector3 LocalScale { get; set; }
        
        void SetParent(ITransform parent); 
    }
}