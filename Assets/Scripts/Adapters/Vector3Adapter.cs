using Interfaces;

namespace Adapters
{
    public class Vector3Adapter : IVector3
    {
        private Data.Vector3 _vector;

        public Vector3Adapter(Data.Vector3 vector)
        {
            _vector = vector;
        }

        public float x
        {
            get => _vector.x;
            set => _vector.x = value;
        }

        public float y
        {
            get => _vector.y;
            set => _vector.y = value;
        }

        public float z
        {
            get => _vector.z;
            set => _vector.z = value;
        }
    }
}