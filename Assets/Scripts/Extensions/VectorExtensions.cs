using Vector2Int = Data.Vector2Int;
using Vector3 = Data.Vector3;

namespace Extensions
{
    public static class VectorExtensions
    {
        public static UnityEngine.Vector3 ToUnityVector3(this Vector3 vector)
        {
            return new UnityEngine.Vector3(vector.x, vector.y, vector.z);
        }

        public static Data.Vector3 ToDataVector3(this UnityEngine.Vector3 vector)
        {
            return new Vector3(vector.x, vector.y, vector.z);
        }

        public static UnityEngine.Vector2Int ToUnityVector2Int(this Vector2Int vector)
        {
            return new UnityEngine.Vector2Int(vector.x, vector.y);
        }

        public static Data.Vector2Int ToDataVector2Int(this UnityEngine.Vector2Int vector)
        {
            return new Vector2Int(vector.x, vector.y);
        }
    }
}