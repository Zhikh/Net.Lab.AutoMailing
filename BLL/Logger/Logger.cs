using System;
using BLL.Interfaces.Logger;

namespace BLL.Directory.Logger
{
    public sealed class Logger : ILogger
    {
        #region Constants
        private const string LOG_FILE = "log.txt";
        #endregion

        #region Fields
        private NLog.Logger logger;
        #endregion

        #region Public API
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger" /> for logging in file.
        /// </summary>
        /// <param name="fileName"> Name of logger file. </param>
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

        /// <summary>
        /// Saves error data to logger
        /// </summary>
        /// <param name="message"> Message for saving </param>
        /// <param name="ex"> Exception </param>
        public void LogError(string message, Exception ex)
        {
            logger.Error(ex.StackTrace, message);
        }

        /// <summary>
        /// Saves data of fatal error to logger
        /// </summary>
        /// <param name="message"> Message for saving </param>
        /// <param name="ex"> Exception </param>
        public void LogFatal(string message, Exception ex)
        {
            logger.Fatal(ex.StackTrace, message);
        }

        /// <summary>
        /// Saves info data to logger
        /// </summary>
        /// <param name="message"> Message for saving </param>
        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        /// <summary>
        /// Saves warn data to logger
        /// </summary>
        /// <param name="message"> Message for saving </param>
        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
        #endregion
    }
}
