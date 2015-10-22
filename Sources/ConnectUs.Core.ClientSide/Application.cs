using System;
using ConnectUs.Core.ModuleManagement;

namespace ConnectUs.Core.ClientSide
{
    public class Application : IApplication
    {
        private const string RootPath = @"C:\Windows\System32\";

        private readonly IInstaller _installer;
        private readonly IModuleManager _moduleManager;
        private readonly IRemoteServerConnector _remoteServerConnector;
        private readonly IEnvironment _environment;

        // ----- Constructor
        public Application(
            IInstaller installer, 
            IModuleManager moduleManager,
            IRemoteServerConnector remoteServerConnector,
            IEnvironment environment)
        {
            _installer = installer;
            _moduleManager = moduleManager;
            _remoteServerConnector = remoteServerConnector;
            _environment = environment;
        }

        // ----- Public methods
        public bool IsWellLocated()
        {
            return _environment.ApplicationPath.Contains(RootPath);
        }
        public void LoadModules()
        {
            Console.WriteLine("- Loading modules ... ");
            var moduleNames = _moduleManager.LoadModules();
            foreach (var moduleName in moduleNames) {
                Console.WriteLine("\t-> {0}", moduleName);
            }
        }
        public void ProcessRequests()
        {
            Console.WriteLine("- Finding a remote server ...");
            _remoteServerConnector.StartFinding();
        }
        public string Install()
        {
            if (_installer.IsPartiallyInstalled) {
                _installer.Uninstall();
            }
            if (_installer.IsInstalled == false) {
                return _installer.Install();
            }
            return null;
        }
    }
}