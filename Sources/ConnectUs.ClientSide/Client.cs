using System;
using ConnectUs.Business;
using ConnectUs.Business.Connections;
using ConnectUs.ClientSide.ModuleManagement;

namespace ConnectUs.ClientSide
{
    public class Client
    {
        private readonly IContinuousRequestProcessor _continuousRequestProcessor;
        private readonly IClientInformation _clientInformation;

        public event EventHandler ClientDisconnected;
        protected virtual void OnClientDisconnected()
        {
            EventHandler handler = ClientDisconnected;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler ClientConnected;
        protected virtual void OnClientConnected()
        {
            EventHandler handler = ClientConnected;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        // ----- Constructors
        static Client()
        {
            InitIoc();
        }
        public Client() : this(Ioc.Instance.GetInstance<IContinuousRequestProcessor>(), Ioc.Instance.GetInstance<IClientInformation>())
        {
        }
        private Client(IContinuousRequestProcessor continuousRequestProcessor, IClientInformation clientInformation)
        {
            _clientInformation = clientInformation;
            _continuousRequestProcessor = continuousRequestProcessor;
            _continuousRequestProcessor.ConnectionLost += ContinuousRequestProcessorOnConnectionLost;
        }

        // ----- Public methods
        public void ConnectToServer(string hostName, int port)
        {
            try {
                var connection = TcpClientConnectionFactory.Build(hostName, port);
                OnClientConnected();
                _continuousRequestProcessor.StartProcessingRequestFromConnection(connection);
                _clientInformation.CurrentConnection = connection;
            }
            catch (ConnectionException) {
                throw new ClientException(string.Format("Unable to connect to the host '{0}' on the port '{1}'.", hostName, port));
            }
        }

        // ----- Event callbacks
        private void ContinuousRequestProcessorOnConnectionLost(object sender, EventArgs args)
        {
            _continuousRequestProcessor.StopProcessingRequestFromConnection();
            OnClientDisconnected();
            _clientInformation.CurrentConnection = null;
        }

        // ----- Utils
        private static void InitIoc()
        {
            Ioc.Instance.Register<IContinuousRequestProcessor, ContinuousRequestProcessor>();
            Ioc.Instance.Register<IClientRequestProcessor, ClientRequestProcessor>();
            Ioc.Instance.Register<IClientRequestHandler, ClientRequestHandler>();
            Ioc.Instance.Register<ICommandLocator, CommandLocator>();
            Ioc.Instance.Register<IModuleManager, ModuleManager>();
            Ioc.Instance.Register<IRequestParser, JsonRequestParser>();
            Ioc.Instance.RegisterSingle<IClientInformation, ClientInformation>();
        }
    }
}