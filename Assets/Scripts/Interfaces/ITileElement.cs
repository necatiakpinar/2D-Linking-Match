using Miscs;

namespace Interfaces
{
    public interface ITileElement
    {
        GameElementType ElementType { get; }
        ITransform Transform { get; }
    }
}