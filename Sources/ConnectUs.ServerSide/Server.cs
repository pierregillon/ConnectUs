using System.Collections.Generic;
using ConnectUs.Business;

namespace ConnectUs.ServerSide
{
    public class Server
    {
        private readonly IConnector _connector;

        public Server(IConnector connector)
        {
            _connector = connector;
            _connector.ClientConnected
        }

        public void Start(int port)
        {
            _connector.Listen(port);
        }

        public IEnumerable<Client> Clients { get; set; }
    }
}