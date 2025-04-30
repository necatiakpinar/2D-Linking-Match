using Adapters;
using Extensions;
using Interfaces;
using UnityEngine;

namespace UnityObjects
{
    public class UnityTransform : ITransform
    {
        private readonly Transform _transform;

        public Transform TransformRef => _transform;

        public UnityTransform(Transform transform)
        {
            _transform = transform;
        }

        public IVector3 Position
        {
            get => new Vector3Adapter(_transform.position.ToDataVector3());
            set => _transform.position = new Vector3(value.x, value.y, value.z);
        }

        public IVector3 LocalPosition
        {
            get => new Vector3Adapter(_transform.localPosition.ToDataVector3());
            set => _transform.localPosition = new Vector3(value.x, value.y, value.z);
        }

        public IQuaternion Rotation
        {
            get => new QuaternionAdapter(_transform.rotation.ToDataQuaternion());
            set => _transform.rotation = new UnityEngine.Quaternion(value.x, value.y, value.z, value.w);
        }

        public IQuaternion LocalRotation 
        {
            get => new QuaternionAdapter(_transform.localRotation.ToDataQuaternion());
            set => _transform.localRotation = new UnityEngine.Quaternion(value.x, value.y, value.z, value.w);
        }

        public IVector3 Scale
        {
            get => new Vector3Adapter(_transform.localScale.ToDataVector3());
            set => _transform.localScale = new Vector3(value.x, value.y, value.z);
        }
        
        public IVector3 LocalScale 
        {
            get => new Vector3Adapter(_transform.localScale.ToDataVector3());
            set => _transform.localScale = new Vector3(value.x, value.y, value.z);
        }

        public void SetParent(ITransform parent)
        {
            var unityTransform = parent as UnityTransform;
            if (unityTransform != null)
            {
                _transform.SetParent(unityTransform._transform);
            }
        }
    }
}