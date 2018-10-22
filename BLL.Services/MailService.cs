using BLL.Interfaces.Logger;
using BLL.Interfaces.MailGenerator;
using BLL.Interfaces.Services;
using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Threading;

namespace BLL.Services
{
    public sealed class MailService : IMailService<string>
    {
        private readonly IMailGenerator<string> _mailGenerator;
        private readonly ILogger _logger;

        public MailService(IMailGenerator<string> mailGenerator, ILogger logger)
        {
            _mailGenerator = mailGenerator ?? throw new ArgumentNullException(nameof(mailGenerator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public bool Send(string fileName)
        {
            bool result = false;
            var smtp = new SmtpClient
            {
                EnableSsl = true
            };

            smtp.SendCompleted += delegate (object sender, AsyncCompletedEventArgs e)
            {
                if (e.Cancelled)
                {
                    _logger.LogInfo($"The message with {fileName} was cancelled.");
                }
                if (e.Error != null)
                {
                    _logger.LogError($"The message with {fileName} wasn't sent.", e.Error);
                }
                else
                {
                    _logger.LogInfo("Message was sent.");
                    result = true;
                }
            };

            try
            {
                smtp.SendMailAsync(_mailGenerator.GenerateMessage(fileName));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }

            Thread.Sleep(1000);
            return result;
        }
    }
}
