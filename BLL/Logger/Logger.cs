using System;
using BLL.Interfaces.Logger;

namespace BLL.Directory.Logger
{
    public sealed class Logger : ILogger
    {
        private const string LOG_FILE = "log.txt";
        private readonly NLog.Logger logger;
        
        public Logger()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();

            var config = new NLog.Config.LoggingConfiguration();
            var logFile = new NLog.Targets.FileTarget("logfile") { FileName = LOG_FILE };
            var logConsole = new NLog.Targets.ConsoleTarget("logconsole") { };

            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Fatal, logFile);
            config.AddRule(NLog.LogLevel.Info, NLog.LogLevel.Warn, logConsole);
            NLog.LogManager.Configuration = config;
        }

        /// <inheritdoc/>
        public void LogError(string message, Exception ex)
        {
            logger.Error(ex.StackTrace, message);
        }

        /// <inheritdoc/>
        public void LogFatal(string message, Exception ex)
        {
            logger.Fatal(ex.StackTrace, message);
        }

        /// <inheritdoc/>
        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        /// <inheritdoc/>
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
