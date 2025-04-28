using Interfaces;
using UnityEngine;
using UnityObjects;
using ILogger = Interfaces.ILogger;

namespace Factories
{
    public class UnityObjectFactory : IObjectFactory
    {
        private readonly ILogger _logger;

        public UnityObjectFactory(ILogger logger)
        {
            _logger = logger;
        }
        
        public T CreateObject<T>(T prefab, ITransform parent, IVector3 localPosition) where T : class
        {
            if (prefab == null)
            {
                _logger.LogError($"Prefab of type {typeof(T).Name} is null!");
                return null;
            }

            var unityParent = (parent as UnityTransform)?.TransformRef;
            var instance = Object.Instantiate(prefab as Object, unityParent);

            if (instance is T spawnable)
            {
                var instanceTransform = (instance as Component)?.transform;
                if (instanceTransform != null)
                    instanceTransform.localPosition = new Vector3(localPosition.x, localPosition.y, localPosition.z);

                return spawnable;
            }

            _logger.LogError($"Instantiated object is not of expected type {typeof(T).Name}");
            return null;
        }
    }
}