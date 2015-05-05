using System.Collections.Generic;
using ConnectUs.Business;

namespace ConnectUs.ServerSide
{
    public class Server
    {
        private readonly IConnectionListener _connectionListener;
        private readonly List<Client> _clients = new List<Client>();

        public IEnumerable<Client> Clients
        {
            get { return _clients; }
        }

        public Server(IConnectionListener connectionListener)
        {
            _connectionListener = connectionListener;
            _connectionListener.ConnectionEstablished += ConnectorOnConnectionEstablished;
        }

        public void Start(int port)
        {
            _connectionListener.StartListening(port);
        }

        private void ConnectorOnConnectionEstablished(object sender, ConnectionEstablishedEventArgs args)
        {
            var client = new Client(args.Connection);
            _clients.Add(client);
            client.RefreshData();
        }
    }
}