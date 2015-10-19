using System.Linq;
using ConnectUs.Core;
using ConnectUs.Core.ClientSide;
using ConnectUs.Core.ModuleManagement;

namespace ConnectUs.ClientSide
{
    public static class Program
    {
        static Program()
        {
            Ioc.Instance.Register<Client>();
            Ioc.Instance.Register<IContinuousRequestProcessor, ContinuousRequestProcessor>();
            Ioc.Instance.Register<IClientRequestProcessor, ClientRequestProcessor>();
            Ioc.Instance.Register<IClientRequestHandler, ClientRequestHandler>();
            Ioc.Instance.Register<ICommandLocator, CommandLocator>();
            Ioc.Instance.RegisterSingle<IModuleManager, ModuleManager>();
            Ioc.Instance.Register<IRequestParser, JsonRequestParser>();
            Ioc.Instance.RegisterSingle<IClientInformation, ClientInformation>();
            Ioc.Instance.RegisterSingle<IApplication, Application>();
        }

        public static void Main(string[] args)
        {
            var application = Ioc.Instance.GetInstance<IApplication>();
            if (args.FirstOrDefault() == "--debug" || application.IsWellLocated()) {
                application.LoadModules();
                application.ProcessRequests();
            }
            else {
                application.Locate();
            }
        }
    }
}