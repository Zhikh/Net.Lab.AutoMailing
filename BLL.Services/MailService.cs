using BLL.Interfaces.Services;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BLL.Services
{
    public sealed class MailService : IMailService<string>
    {
        public void Send(string fileName)
        {
            //Авторизация на SMTP сервере
            SmtpClient Smtp = new SmtpClient("smtp.mail.ru", 2525);
            Smtp.Credentials = new NetworkCredential("auto_mailler@mail.ru", "Y4R6N4d2C4");
            Smtp.EnableSsl = true;

            //Формирование письма
            MailMessage Message = new MailMessage();
            Message.From = new MailAddress("auto_mailler@mail.ru");
            Message.To.Add(new MailAddress("zhikhanastasya@gmail.com"));
            Message.Subject = "Заголовок";
            Message.Body = "Сообщение";

            Attachment attach = new Attachment(fileName, MediaTypeNames.Application.Octet);

            // Добавляем информацию для файла
            ContentDisposition disposition = attach.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(fileName);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(fileName);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(fileName);

            Message.Attachments.Add(attach);
            Smtp.Send(Message);//отправка
        }
    }
}
