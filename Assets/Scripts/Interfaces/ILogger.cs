namespace Interfaces
{
    public interface ILogger
    {
        void Log(object message);
        void LogError(object message);
        void LogWarning(object message);
    }
}