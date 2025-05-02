namespace Interfaces
{
    public interface ICryptoHelper
    {
        public string Encrypt(string data);
        public string Decrypt(string data);
    }
}