using System.Collections.Generic;

namespace ConnectUs.ServerSide
{
    public class Server
    {
        private readonly List<Client> _clients = new List<Client>();

        public void AddConnectedClient(Client client)
        {
            _clients.Add(client);
        }

        public IEnumerable<Client> GetConnectedClients()
        {
            return _clients;
        }
    }
}