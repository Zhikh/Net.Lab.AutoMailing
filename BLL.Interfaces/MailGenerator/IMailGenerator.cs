using System.Net.Mail;

namespace BLL.Interfaces.MailGenerator
{
    public interface IMailGenerator<T>
    {
        /// <summary>
        /// Generates mail message with data of type <see cref="T"/>
        /// </summary>
        /// <param name="data"> Data for sending </param>
        /// <returns> Instance of <see cref="MailMessage"/> </returns>
        MailMessage GenerateMessage(T data);
    }
}
