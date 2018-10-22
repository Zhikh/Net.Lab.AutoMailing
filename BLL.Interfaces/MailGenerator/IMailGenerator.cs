using System.Net.Mail;

namespace BLL.Interfaces.MailGenerator
{
    public interface IMailGenerator<T>
    {
        MailMessage GenerateMessage(T data);
    }
}
