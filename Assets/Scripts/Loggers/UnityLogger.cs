using UnityEngine;
using ILogger = Interfaces.ILogger;

namespace Loggers
{
    public class UnityLogger : ILogger
    {
        public void Log(object message)
        {
            Debug.Log(message);
        }

        public void LogError(object message)
        {
            Debug.LogError(message);
        }

        public void LogWarning(object message)
        {
            Debug.LogWarning(message);
        }
    }
}