using ConnectUs.Business;

namespace ConnectUs.ClientSide
{
    public class Client
    {
        private readonly IConnector _connector;

        public Client(IConnector connector)
        {
            _connector = connector;
        }

        public void Connect(string host, int port)
        {
            _connector.Connect(host, port);
        }
    }

}