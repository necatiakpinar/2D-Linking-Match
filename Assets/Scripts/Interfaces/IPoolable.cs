namespace Interfaces
{
    public interface IPoolable<T> where T : IPoolable<T>
    {
        void OnSpawn();
        void ReturnToPool(T poolObject);
    }
}