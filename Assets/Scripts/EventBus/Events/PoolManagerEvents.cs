using Data.Models;
using Interfaces;
using Miscs;
using UnityEngine;

namespace EventBus.Events
{
    public struct SpawnGameplayElementPoolEvent : IEvent
    {
        public ElementModel ElementModel;
        public Vector2 Position;
        public Quaternion Rotation;
        public Transform Parent;
        
        public SpawnGameplayElementPoolEvent(ElementModel elementModel, Vector2 position, Quaternion rotation = default, Transform parent = null)
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