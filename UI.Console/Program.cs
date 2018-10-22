using BLL.Interfaces.Services;
using BLL.Interfaces.SetUp;
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
            ISetUpManager setUpManager = _resolver.Get<ISetUpManager>();

            System.Console.WriteLine("Press \'q\' to exit.");
            while (System.Console.Read() != 'q')
            {
                if (System.Console.ReadLine() != "edit'")
                {
                    System.Console.WriteLine("Add receiver:");
                    var receiver = System.Console.ReadLine();
                    setUpManager.AddUpdateSetting(SetUpConstants.ReceiverMailAddress, receiver);
                }
            };
        }
    }
}
