using Data.Models;
using Interfaces;
using Miscs;
using UnityEngine;

namespace EventBus.Events
{
    public struct SpawnGameplayElementPoolEvent : IEvent
    {
        public readonly ElementModel ElementModel;
        public readonly ITile Tile;
        public readonly IVector3 Position;
        public readonly IQuaternion Rotation;
        public readonly ITransform Parent;
        
        public SpawnGameplayElementPoolEvent(ElementModel elementModel, ITile tile, IVector3 position, IQuaternion rotation = default, ITransform parent = null)
        {
            ElementModel = elementModel;
            Tile = tile;
            Position = position;
            Rotation = rotation;
            Parent = parent;
        }
    }
    
    public struct ReturnToPoolEvent<T> : IEvent where T : MonoBehaviour
    {
        public readonly GameElementType ElementType;
        public readonly T MonoBehaviour;
        
        public ReturnToPoolEvent(GameElementType elementType, T monoBehaviour)
        {
            ElementType = elementType;
            MonoBehaviour = monoBehaviour;
        }
    }
}