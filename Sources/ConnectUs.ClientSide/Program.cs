using System.Diagnostics;
using ConnectUs.Core;
using ConnectUs.Core.ClientSide;
using ConnectUs.Core.ModuleManagement;

namespace ConnectUs.ClientSide
{
    public static class Program
    {
        static Program()
        {
            Ioc.Instance.RegisterSingle<IApplication, Application>();
            Ioc.Instance.Register<IInstaller, Installer>();
            Ioc.Instance.Register<IEnvironment, ClientEnvironment>();
            Ioc.Instance.Register<IFileService, FileService>();
            Ioc.Instance.Register<IRegistry, WindowsRegistry>();
            Ioc.Instance.Register<IRemoteServerConnector, RemoteServerConnector>();
            Ioc.Instance.Register<IContinuousRequestProcessor, ContinuousRequestProcessor>();
            Ioc.Instance.Register<IClientRequestProcessor, ClientRequestProcessor>();
            Ioc.Instance.Register<IClientRequestHandler, ClientRequestHandler>();
            Ioc.Instance.Register<ICommandLocator, CommandLocator>();
            Ioc.Instance.RegisterSingle<IModuleManager, ModuleManager>();
            Ioc.Instance.Register<IRequestParser, JsonRequestParser>();
            Ioc.Instance.RegisterSingle<IClientInformation, ClientInformation>();
        }

        public static void Main(string[] args)
        {
            var application = Ioc.Instance.GetInstance<IApplication>();
            if (application.IsWellLocated()) {
                application.LoadModules();
                application.ProcessRequests();
            }
            else {
                var filePath = application.Install();
                if (string.IsNullOrEmpty(filePath) == false) {
                    Process.Start(filePath);
                }
            }
        }
    }
}