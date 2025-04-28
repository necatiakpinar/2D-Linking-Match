using System.Collections.Generic;
using Loggers;
using ILogger = Interfaces.ILogger;

namespace Helpers
{
    public static class LoggerUtil
    {
        private static readonly List<ILogger> _loggers = new()
        {
            new UnityLogger()
        };

        public static void Log(object message)
        {
            ILogger logger;
            for (var i = 0; i < _loggers.Count; i++)
            {
                logger = _loggers[i];
                logger.Log(message);
            }
        }

        public static void LogError(object message)
        {
            ILogger logger;
            for (var i = 0; i < _loggers.Count; i++)
            {
                logger = _loggers[i];
                logger.LogError(message);
            }
        }

        public static void LogWarning(object message)
        {
            ILogger logger;
            for (var i = 0; i < _loggers.Count; i++)
            {
                logger = _loggers[i];
                logger.LogWarning(message);
            }
        }
    }
}