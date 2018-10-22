using System;
using System.IO;
using BLL.Interfaces.Directoty;
using BLL.Interfaces.SetUp;

namespace BLL.Directory
{
    public sealed class DirectoryWatcher : IDirectoryWatcher
    {
        private const string EXTENSION = "*.*";

        private ISetUpManager _setUpManager;

        public DirectoryWatcher(ISetUpManager setUpManager)
        {
            _setUpManager = setUpManager ?? throw new ArgumentNullException(nameof(setUpManager));
        }

        /// <inheritdoc/>
        public event FileSystemEventHandler FileCreated = delegate { };

        /// <inheritdoc/>
        public event FileSystemEventHandler FileDeleted = delegate { };

        /// <inheritdoc/>
        public void Run()
        {
            var watcher = new FileSystemWatcher
            {
                Path = _setUpManager.ReadSetting(SetUpConstants.SourcePath),
                Filter = EXTENSION
            };

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
