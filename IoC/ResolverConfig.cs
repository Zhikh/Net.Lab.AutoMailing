using BLL.Configuration;
using BLL.Directory;
using BLL.Directory.Logger;
using BLL.Directory.MailGenerator;
using BLL.Interfaces.Directoty;
using BLL.Interfaces.Logger;
using BLL.Interfaces.MailGenerator;
using BLL.Interfaces.Services;
using BLL.Interfaces.SetUp;
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
            // strategies
            kernel.Bind<ISetUpManager>().To<SetUpManager>();

            kernel.Bind<IMailGenerator<string>>().To<MailGenerator>();

            kernel.Bind<IDirectoryWatcher>().To<DirectoryWatcher>()
                .WithConstructorArgument("setUpManager", kernel.Get<ISetUpManager>());

            kernel.Bind<ILogger>().To<Logger>();

            // services
            kernel.Bind<IMailService<string>>().To<MailService>()
                .WithConstructorArgument("setUpManager", kernel.Get<ISetUpManager>())
                .WithConstructorArgument("mailGenerator", kernel.Get<IMailGenerator<string>>())
                .WithConstructorArgument("logger", kernel.Get<ILogger>());

            kernel.Bind<IDirectoryService>().To<DirectoryService>()
                .WithConstructorArgument("watcher", kernel.Get<IDirectoryWatcher>())
                .WithConstructorArgument("mailService", kernel.Get<IMailService<string>>())
                .WithConstructorArgument("setUpManager", kernel.Get<ISetUpManager>());
        }
    }
}
