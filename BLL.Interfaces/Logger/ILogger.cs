using System;

namespace BLL.Interfaces.Logger
{
    public interface ILogger
    {
        /// <summary>
        /// Saves info data to logger
        /// </summary>
        /// <param name="message"> Message for saving </param>
        void LogInfo(string message);

        /// <summary>
        /// Saves warn data to logger
        /// </summary>
        /// <param name="message"> Message for saving </param>
        void LogWarn(string message);

        /// <summary>
        /// Saves error data to logger
        /// </summary>
        /// <param name="message"> Message for saving </param>
        /// <param name="ex"> Exception </param>
        void LogError(string message, Exception ex);

        /// <summary>
        /// Saves data of fatal error to logger
        /// </summary>
        /// <param name="message"> Message for saving </param>
        /// <param name="ex"> Exception </param>
        void LogFatal(string message, Exception ex);
    }
}
