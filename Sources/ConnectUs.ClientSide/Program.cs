using System;
using System.IO;
using System.Threading;
using ConnectUs.Business;
using ConnectUs.ClientSide.ModuleManagement;

namespace ConnectUs.ClientSide
{
    public static class Program
    {
        const string HostName = "localhost";
        const int Port = 9000;
        private static readonly AutoResetEvent ResetEvent = new AutoResetEvent(false);

        public static void Main(string[] args)
        {
            InitIoc();
            LoadExistingModules();
            StartClient();
        }

        private static void InitIoc()
        {
            Ioc.Instance.Register<Client>();
            Ioc.Instance.Register<IContinuousRequestProcessor, ContinuousRequestProcessor>();
            Ioc.Instance.Register<IClientRequestProcessor, ClientRequestProcessor>();
            Ioc.Instance.Register<IClientRequestHandler, ClientRequestHandler>();
            Ioc.Instance.Register<ICommandLocator, CommandLocator>();
            Ioc.Instance.RegisterSingle<IModuleManager, ModuleManager>();
            Ioc.Instance.Register<IRequestParser, JsonRequestParser>();
            Ioc.Instance.RegisterSingle<IClientInformation, ClientInformation>();
        }

        private static void LoadExistingModules()
        {
            Console.WriteLine("- Loading modules");
            var moduleManager = Ioc.Instance.GetInstance<IModuleManager>();
            foreach (var filePath in Directory.GetFiles("Modules")) {
                try {
                    Console.Write("\t-> {0} ... ", Path.GetFileName(filePath));
                    moduleManager.AddModule(filePath);
                    Console.WriteLine("OK");
                }
                catch (Exception) {
                    Console.WriteLine("ERROR");
                }
            }
        }

        private static void StartClient()
        {
            Console.WriteLine("- Starting client ...");

            var client = Ioc.Instance.GetInstance<Client>();
            client.ClientConnected += ClientOnClientConnected;
            client.ClientDisconnected += ClientOnClientDisconnected;

            while (true)
            {
                Console.WriteLine("Trying to connect to the host '{0}' on port '{1}'", HostName, Port);
                try
                {
                    client.ConnectToServer(HostName, Port);
                    ResetEvent.WaitOne();
                }
                catch (ClientException)
                {
                }
            }
        }
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