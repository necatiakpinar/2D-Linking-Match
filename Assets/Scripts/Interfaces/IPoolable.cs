namespace Interfaces
{
    public interface IPoolable<T> where T : IPoolable<T>
    {
        ITransform Transform { get; }
        void OnSpawn();
        void ReturnToPool(T poolObject);
    }
}