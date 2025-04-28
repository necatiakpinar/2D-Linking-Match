using System;
using Interfaces;
using UnityEngine;

namespace Miscs
{
    [Serializable]
    public class PoolObject<T> where T : IPoolable<T>
    {
        [SerializeField] private GameElementType _elementType;
        [SerializeField] private T _objectPf;
        [SerializeField] private int _size;

        public GameElementType ElementType => _elementType;
        public T ObjectPf => _objectPf;
        public int Size => _size;
    }
}