using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Threading;
using BLL.Interfaces.Logger;
using BLL.Interfaces.MailGenerator;
using BLL.Interfaces.Services;
using BLL.Interfaces.SetUp;

namespace BLL.Services
{
    public sealed class MailService : IMailService<string>
    {
        private readonly IMailGenerator<string> _mailGenerator;
        private readonly ILogger _logger;
        private readonly ISetUpManager _setUpManager;

        public MailService(IMailGenerator<string> mailGenerator, ILogger logger, ISetUpManager setUpManager)
        {
            _mailGenerator = mailGenerator ?? throw new ArgumentNullException(nameof(mailGenerator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _setUpManager = setUpManager ?? throw new ArgumentNullException(nameof(setUpManager));
        }

        /// <inheritdoc/>
        public bool Send(string fileName)
        {
            bool result = false;
            var smtp = new SmtpClient
            {
                EnableSsl = true
            };

            smtp.SendCompleted += delegate(object sender, AsyncCompletedEventArgs e)
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

            using (var mail = _mailGenerator.GenerateMessage(fileName))
            {
                smtp.SendMailAsync(mail);

                try
                { 
                    var timeOut = int.Parse(_setUpManager.ReadSetting(SetUpConstants.TimeOut));
                
                    Thread.Sleep(timeOut);
                }
                catch(Exception ex)
                {
                    _logger.LogError("TimeOut value hasn't been parsed.", ex);
                }
            }

            return result;
        }
    }
}
