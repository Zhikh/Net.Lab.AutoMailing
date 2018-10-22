using BLL.Interfaces.Services;
using IoC;
using Ninject;

namespace UI.Console
{
    class Program
    {
        private static readonly IKernel _resolver;

        static Program()
        {
            _resolver = new StandardKernel();
            _resolver.ConfigurateResolver();
        }

        public static void Main(string[] args)
        {
            IDirectoryService service = _resolver.Get<IDirectoryService>();
            service.StartWatching();

            System.Console.WriteLine("Press \'q\' to exit.");
            while (System.Console.Read() != 'q');
        }
    }
}
