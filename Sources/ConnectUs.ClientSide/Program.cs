using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
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
            if (IsAdministrator() == false) {
                RestartApplicationWithAdministratorPrivileges();
                return;
            }
            var application = Ioc.Instance.GetInstance<IApplication>();
            if (args.Any(x => x == "--debug" || application.IsWellLocated())) {
                application.LoadModules();
                application.ProcessRequests();
            }
            else {
                var filePath = application.Install();
                if (string.IsNullOrEmpty(filePath) == false) {
                    StartExecutableWithAdministratorPrivileges(filePath);
                }
            }
        }

        // ----- Utils
        private static bool IsAdministrator()
        {
            using (var identity = WindowsIdentity.GetCurrent()) {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
        private static void RestartApplicationWithAdministratorPrivileges()
        {
            var exeName = Process.GetCurrentProcess().MainModule.FileName;
            StartExecutableWithAdministratorPrivileges(exeName);
        }
        private static void StartExecutableWithAdministratorPrivileges(string filePath)
        {
            var startInfo = new ProcessStartInfo(filePath)
            {
                Verb = "runas"
            };
            Process.Start(startInfo);
        }
    }
}