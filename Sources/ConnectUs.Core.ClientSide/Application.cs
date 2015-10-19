using System;
using ConnectUs.Core.ModuleManagement;

namespace ConnectUs.Core.ClientSide
{
    public class Application : IApplication
    {
        private readonly IInstaller _installer;
        private readonly IModuleManager _moduleManager;
        private readonly IRemoteServerConnector _remoteServerConnector;

        // ----- Constructor
        public Application(
            IInstaller installer, 
            IModuleManager moduleManager,
            IRemoteServerConnector remoteServerConnector)
        {
            _installer = installer;
            _moduleManager = moduleManager;
            _remoteServerConnector = remoteServerConnector;
        }

        // ----- Public methods
        public bool IsWellLocated()
        {
            return _installer.IsInstalled;
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
            return _installer.Install();
        }
    }
}