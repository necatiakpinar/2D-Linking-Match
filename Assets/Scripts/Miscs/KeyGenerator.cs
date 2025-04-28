using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.Device;

namespace Miscs
{
    public static class KeyGenerator
    {
        public static string GenerateDeviceKey()
        {
            string deviceIdentifier = SystemInfo.deviceUniqueIdentifier;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(deviceIdentifier));
                return Convert.ToBase64String(hash).Substring(0, 32); // 32 byte AES key
            }
        }
    }
}