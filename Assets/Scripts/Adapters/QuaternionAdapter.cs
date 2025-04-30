using Interfaces;

namespace Adapters
{
    public class QuaternionAdapter: IQuaternion
    {
        private readonly Data.Quaternion _quaternion;
        
        public QuaternionAdapter(Data.Quaternion quaternion)
        {
            _quaternion = quaternion;
        }

        public float x
        {
            get => _quaternion.x;
            set => _quaternion.x = value;
        }
        public float y
        {
            get => _quaternion.y;
            set => _quaternion.y = value;
        }
        public float z
        {
            get => _quaternion.z;
            set => _quaternion.z = value;
        }
        public float w
        {
            get => _quaternion.w;
            set => _quaternion.w = value;
        }
    }
}