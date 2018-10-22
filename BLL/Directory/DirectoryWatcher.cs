using BLL.Interfaces.Directoty;
using BLL.Interfaces.SetUp;
using System;
using System.IO;

namespace BLL.Directory
{
    public sealed class DirectoryWatcher : IDirectoryWatcher
    {
        public event FileSystemEventHandler FileCreated = delegate { };
        public event FileSystemEventHandler FileDeleted = delegate { };

        private const string EXTENSION = "*.*";
        private ISetUpManager _setUpManager;

        public DirectoryWatcher(ISetUpManager setUpManager)
        {
            _setUpManager = setUpManager ?? throw new ArgumentNullException(nameof(setUpManager));
        }

        public void Run()
        {
            var watcher = new FileSystemWatcher();

            watcher.Path = _setUpManager.ReadSetting(SetUpConstants.SourcePath);
            watcher.Filter = EXTENSION;

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
