using System.IO;

namespace BLL.Interfaces.Directoty
{
    public interface IDirectoryWatcher
    {
        event FileSystemEventHandler FileCreated;

        event FileSystemEventHandler FileDeleted;

        void Run();
    }
}
