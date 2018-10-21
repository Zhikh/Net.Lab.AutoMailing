using BLL.Directory;
using BLL.Interfaces.Directoty;
using BLL.Interfaces.Services;
using BLL.Services;
using Ninject;

namespace IoC
{
    public static class ResolverConfig
    {
        /// <summary>
        /// Creates bindings.
        /// </summary>
        /// <param name="kernel">  Provides a path to register bindings. </param>
        public static void ConfigurateResolver(this IKernel kernel)
        {
            // directory
            kernel.Bind<IDirectoryWatcher>().To<DirectoryWatcher>();

            // services
            kernel.Bind<IDirectoryService>().To<DirectoryService>()
                .WithConstructorArgument("watcher", kernel.Get<IDirectoryWatcher>());

            // strategy
        }
    }
}
