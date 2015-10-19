using System;
using ConnectUs.Core.ModuleManagement;

namespace ConnectUs.Core.ClientSide
{
    public class Application : IApplication
    {
        private readonly IModuleManager _moduleManager;
        private readonly IRemoteServerConnector _remoteServerConnector;

        // ----- Constructor
        public Application(IModuleManager moduleManager, IRemoteServerConnector remoteServerConnector)
        {
            _moduleManager = moduleManager;
            _remoteServerConnector = remoteServerConnector;
        }

        // ----- Public methods
        public bool IsWellLocated()
        {
            throw new NotImplementedException();
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
        public void Locate()
        {
            throw new NotImplementedException();
        }
    }
}