namespace Interfaces
{
    public interface IJsonHelper
    {
        public string ToJson(object obj, bool prettyPrint);
        public object FromJson(string json, System.Type type);
    }
}