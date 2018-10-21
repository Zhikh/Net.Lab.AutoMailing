using System;
using System.IO;

namespace BLL.Interfaces.Directoty
{
    public interface IDirectoryWatcher
    {
        void Run();

        event FileSystemEventHandler FileCreated;

        event FileSystemEventHandler FileDeleted;
    }
}
