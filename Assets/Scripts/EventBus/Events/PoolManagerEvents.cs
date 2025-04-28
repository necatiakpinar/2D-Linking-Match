using Data.Models;
using Interfaces;
using Miscs;
using UnityEngine;

namespace EventBus.Events
{
    public struct SpawnGameplayElementPoolEvent : IEvent
    {
        public ElementModel ElementModel;
        public IVector3 Position;
        public Quaternion Rotation;
        public ITransform Parent;
        
        public SpawnGameplayElementPoolEvent(ElementModel elementModel, IVector3 position, Quaternion rotation = default, ITransform parent = null)
        {
            ElementModel = elementModel;
            Position = position;
            Rotation = rotation;
            Parent = parent;
        }
    }
    
    public struct ReturnToPoolEvent<T> : IEvent where T : MonoBehaviour
    {
        public GameElementType ElementType;
        public T MonoBehaviour;
        
        public ReturnToPoolEvent(GameElementType elementType, T monoBehaviour)
        {
            ElementType = elementType;
            MonoBehaviour = monoBehaviour;
        }
    }
}