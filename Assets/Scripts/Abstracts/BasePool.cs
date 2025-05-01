using System.Collections.Generic;
using Helpers;
using Interfaces;
using Miscs;
using UnityEngine;

namespace Abstracts
{
    public abstract class BasePool<T> : MonoBehaviour where T : MonoBehaviour, IPoolable<T>
    {
        [SerializeField] private List<PoolObject<T>> _poolObjects;

        private Dictionary<GameElementType, Queue<T>> _poolDictionary;
        private Dictionary<GameElementType, T> _prefabDictionary; 

        private void OnEnable()
        {
            _poolDictionary = new Dictionary<GameElementType, Queue<T>>();
            _prefabDictionary = new Dictionary<GameElementType, T>();

            foreach (var poolObject in _poolObjects)
            {
                _poolDictionary[poolObject.ElementType] = new Queue<T>();
                _prefabDictionary[poolObject.ElementType] = poolObject.ObjectPf;
            }
        }

        private void OnDisable()
        {
            ClearAllPools();
        }

        public T SpawnFromPool(GameElementType elementType, IVector3 position, IQuaternion rotation, ITransform parent = null)
        {
            if (!_poolDictionary.TryGetValue(elementType, out var objectQueue))
            {
                LoggerUtil.LogWarning($"No pool with ID {elementType} found!");
                return null;
            }

            T objectToSpawn;

            if (objectQueue.Count == 0)
            {
                if (!_prefabDictionary.TryGetValue(elementType, out var prefab))
                {
                    LoggerUtil.LogWarning($"No prefab found for element type {elementType}!");
                    return null;
                }

                objectToSpawn = Instantiate(prefab);
                objectToSpawn.gameObject.SetActive(false);
            }
            else
            {
                objectToSpawn = objectQueue.Dequeue();
            }

            if (parent != null)
                objectToSpawn.Transform.SetParent(parent);

            objectToSpawn.Transform.LocalPosition = position;
            objectToSpawn.Transform.Rotation = rotation;
            objectToSpawn.gameObject.SetActive(true);
            return objectToSpawn;
        }

        public void ReturnToPool(GameElementType elementType, T obj)
        {
            if (!_poolDictionary.ContainsKey(elementType))
            {
                LoggerUtil.LogWarning($"No pool with ID {elementType} found!");
                return;
            }

            obj.gameObject.SetActive(false);
            _poolDictionary[elementType].Enqueue(obj);
        }

        public void ClearAllPools()
        {
            foreach (var keyValuePair in _poolDictionary)
            {
                var objectQueue = keyValuePair.Value;

                while (objectQueue.Count > 0)
                {
                    T obj = objectQueue.Dequeue();
                    if (obj == null)
                        continue;

                    Destroy(obj.gameObject);
                }
            }

            _poolDictionary.Clear();
        }
    }
}