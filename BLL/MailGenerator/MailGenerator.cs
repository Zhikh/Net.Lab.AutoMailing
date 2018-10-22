using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using BLL.Interfaces.MailGenerator;
using BLL.Interfaces.SetUp;

namespace BLL.Directory.MailGenerator
{
    public sealed class MailGenerator : IMailGenerator<string>
    {
        private readonly ISetUpManager _setUpManager;

        public MailGenerator(ISetUpManager setUpManager)
        {
            _setUpManager = setUpManager ?? throw new ArgumentNullException(nameof(setUpManager));
        }

        /// <inheritdoc/>
        public MailMessage GenerateMessage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Name of file can't be null or empty!", nameof(fileName));
            }

            var mail = new MailMessage();
            
            mail.To.Add(new MailAddress(_setUpManager.ReadSetting(SetUpConstants.ReceiverMailAddress)));
            mail.Subject = "The file was added.";
            mail.Body = $"The file {fileName} was added.";

            var attach = new Attachment(fileName, MediaTypeNames.Application.Octet);

            CreateAttachment(fileName, attach);
            mail.Attachments.Add(attach);

            return mail;
        }

        private void CreateAttachment(string fileName, Attachment attachment)
        {
            ContentDisposition disposition = attachment.ContentDisposition;
            
            disposition.CreationDate = File.GetCreationTime(fileName);
            disposition.ModificationDate = File.GetLastWriteTime(fileName);
            disposition.ReadDate = File.GetLastAccessTime(fileName);
        }
    }
}
