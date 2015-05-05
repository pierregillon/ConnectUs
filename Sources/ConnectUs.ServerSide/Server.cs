using System.Collections.Generic;

namespace ConnectUs.ServerSide
{
    public class Server
    {
        private readonly IClientListener _clientListener;
        private readonly List<Client> _clients = new List<Client>();

        public Server(IClientListener clientListener)
        {
            _clientListener = clientListener;
            _clientListener.ClientConnected+=ClientListenerOnClientConnected;
        }

        private void ClientListenerOnClientConnected(object sender, ClientConnectedEventArgs args)
        {
            _clients.Add(args.Client);
        }

        public IEnumerable<Client> GetConnectedClients()
        {
            return _clients;
        }
    }
}