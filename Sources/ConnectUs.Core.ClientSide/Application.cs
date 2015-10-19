using System;
using System.IO;
using System.Threading;
using ConnectUs.Core.ModuleManagement;

namespace ConnectUs.Core.ClientSide
{
    public class Application : IApplication
    {
        private const string HostName = "localhost";
        private const int Port = 9000;
        private static readonly AutoResetEvent ResetEvent = new AutoResetEvent(false);

        private readonly IModuleManager _moduleManager;
        private readonly Client _client;

        // ----- Constructor
        public Application(IModuleManager moduleManager, Client client)
        {
            _moduleManager = moduleManager;

            _client = client;
            _client.ClientConnected += ClientOnClientConnected;
            _client.ClientDisconnected += ClientOnClientDisconnected;
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
            Console.WriteLine("- Starting client ...");
            while (true) {
                Console.WriteLine("Trying to connect to the host '{0}' on port '{1}'", HostName, Port);
                try {
                    _client.ConnectToServer(HostName, Port);
                    ResetEvent.WaitOne();
                }
                catch (ClientException) {}
            }
        }
        public void Locate()
        {
            throw new NotImplementedException();
        }

        // ----- Event handlers
        private static void ClientOnClientConnected(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Client connected.");
        }
        private static void ClientOnClientDisconnected(object sender, EventArgs eventArgs)
        {
            Console.WriteLine("Client disconnected.");
            ResetEvent.Set();
        }
    }
}