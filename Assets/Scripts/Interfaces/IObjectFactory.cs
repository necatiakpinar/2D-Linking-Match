namespace Interfaces
{
    public interface IObjectFactory
    {
        T CreateObject<T>(T prefab, ITransform parent, IVector3 localPosition) where T : class;
    }
}