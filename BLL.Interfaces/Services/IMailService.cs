using System;

namespace BLL.Interfaces.Services
{
    public interface IMailService<in T>
    {
        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="data"> Data for sending </param>
        /// <returns> It's true if message was sent, otherwise - false </returns>
        bool Send(T data);
    }
}
