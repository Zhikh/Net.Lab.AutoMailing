﻿using BLL.Interfaces.Directoty;
using BLL.Interfaces.Logger;
using BLL.Interfaces.Services;
using BLL.Interfaces.SetUp;
using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading;

namespace BLL.Services
{
    public sealed class DirectoryService: IDirectoryService
    {
        private const int TIME_OUT = 1000;
        private readonly IDirectoryWatcher _watcher;
        private readonly IMailService<string> _mailService;
        private readonly ISetUpManager _setUpManager;
        private readonly ILogger _logger;

        private string[] extensions = { ".txt", ".docx" };

        public DirectoryService(IDirectoryWatcher watcher, IMailService<string> mailService, ISetUpManager setUpManager, ILogger logger)
        {
            _watcher = watcher ?? throw new ArgumentNullException(nameof(watcher));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _setUpManager = setUpManager ?? throw new ArgumentNullException(nameof(setUpManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void StartWatching()
        {
            _watcher.FileCreated += new FileSystemEventHandler(OnCreated);
            _watcher.FileDeleted += new FileSystemEventHandler(OnChanged);

            try
            {
                _watcher.Run();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            var extension = (Path.GetExtension(e.FullPath) ?? string.Empty).ToLower();

            if (extensions.Any(extension.Equals))
            {
                try
                {
                    if (File.Exists(e.FullPath) && _mailService.Send(e.FullPath))
                    {
                        Thread.Sleep(TIME_OUT);
                        File.Delete(e.FullPath);
                    }
                }
                catch (SmtpFailedRecipientsException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                }
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            _logger.LogInfo("File: " + e.FullPath + " " + e.ChangeType);
        }
    }
}
