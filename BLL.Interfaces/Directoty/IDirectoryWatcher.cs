using System.IO;

namespace BLL.Interfaces.Directoty
{
    public interface IDirectoryWatcher
    {
        /// <summary>
        /// Occurs when a file is created.
        /// </summary>
        event FileSystemEventHandler FileCreated;

        /// <summary>
        /// Occurs when a file is deleted.
        /// </summary>
        event FileSystemEventHandler FileDeleted;

        /// <summary>
        /// Starts monitoring process.
        /// </summary>
        void Run();
    }
}
