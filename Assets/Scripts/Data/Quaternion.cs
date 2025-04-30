using Interfaces;

namespace Data
{
    public class Quaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;

        public Quaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public Quaternion(IVector3 vector3, float w)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
            this.w = w;
        }
    }
}