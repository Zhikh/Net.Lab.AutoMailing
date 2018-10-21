using BLL.Interfaces.Directoty;
using System;
using System.IO;

namespace BLL.Directory
{
    public sealed class DirectoryWatcher : IDirectoryWatcher
    {
        public event FileSystemEventHandler FileCreated = delegate { };
        public event FileSystemEventHandler FileDeleted = delegate { };

        public void Run()
        {
            var watcher = new FileSystemWatcher();

            watcher.Path = @"D:\test\";
            watcher.Filter = "*.txt"; 

            watcher.Created += new FileSystemEventHandler(OnCreated);
            watcher.Deleted += new FileSystemEventHandler(OnDeleted);

            watcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object sender, FileSystemEventArgs eventArgs)
        {
            FileCreated?.Invoke(sender, eventArgs);
        }

        private void OnDeleted(object sender, FileSystemEventArgs eventArgs)
        {
            FileDeleted?.Invoke(sender, eventArgs);
        }
    }
}
