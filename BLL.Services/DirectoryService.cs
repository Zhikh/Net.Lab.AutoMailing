using BLL.Interfaces.Args;
using BLL.Interfaces.Directoty;
using BLL.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public sealed class DirectoryService: IDirectoryService
    {
        private IDirectoryWatcher _watcher;
        private IMailService<string> _mailService;

        public DirectoryService(IDirectoryWatcher watcher, IMailService<string> mailService)
        {
            _watcher = watcher ?? throw new ArgumentNullException(nameof(watcher));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
        }

        public void StartWatching()
        {
            _watcher.FileCreated += new FileSystemEventHandler(OnCreated);
            _watcher.FileDeleted += new FileSystemEventHandler(OnChanged);

            _watcher.Run();
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            int count = 0;

            //копирование фалов 
            DirectoryInfo sourc = new DirectoryInfo(@"D:\test\");

            foreach (FileInfo item in sourc.GetFiles())
            {
                if (_mailService.Send(sourc + item.Name))
                {
                    File.Delete(sourc + item.Name);
                }

                count++;
            }

            //  
            if (count > 1) System.Console.WriteLine(count);
            else if (count == 1) System.Console.WriteLine(count);
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            System.Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }

        private void OnSendCompleted(object sender, MailArgs eventArgs)
        {
            if (eventArgs.IsCompleted && File.Exists(eventArgs.FileName))
            {
                File.Delete(eventArgs.FileName);
            }
        }
    }
}
