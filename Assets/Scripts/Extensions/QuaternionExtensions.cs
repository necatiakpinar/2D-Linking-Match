namespace Extensions
{
    public static class QuaternionExtensions
    {
        public static Data.Quaternion ToDataQuaternion(this UnityEngine.Quaternion quaternion)
        {
            return new Data.Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }
        
        public static UnityEngine.Quaternion ToUnityQuaternion(this Interfaces.IQuaternion quaternion)
        {
            return new UnityEngine.Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
        }
    }
}