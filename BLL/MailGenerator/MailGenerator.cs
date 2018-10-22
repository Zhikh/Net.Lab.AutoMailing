using BLL.Interfaces.MailGenerator;
using BLL.Interfaces.SetUp;
using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace BLL.Directory.MailGenerator
{
    public sealed class MailGenerator : IMailGenerator<string>
    {
        private ISetUpManager _setUpManager;

        public MailGenerator(ISetUpManager setUpManager)
        {
            _setUpManager = setUpManager ?? throw new ArgumentNullException(nameof(setUpManager));
        }

        public MailMessage GenerateMessage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Name of file can't be null or empty!", nameof(fileName));
            }

            var Message = new MailMessage();

            Message.To.Add(new MailAddress(_setUpManager.ReadSetting(SetUpConstants.ReceiverMailAddress)));
            Message.Subject = "The file was added.";
            Message.Body = $"The file {fileName} was added.";

            var attach = new Attachment(fileName, MediaTypeNames.Application.Octet);

            CreateAttachment(fileName, attach);

            Message.Attachments.Add(attach);
            return Message;
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
