using System.Net;
using ConnectUs.Business;

namespace ConnectUs.ServerSide
{
    public class Client
    {
        private readonly IConnection _connection;
        public IPAddress Ip { get; private set; }

        public Client(IConnection connection)
        {
            _connection = connection;
        }
        public void RefreshData()
        {
             _connection.Send(new ClientInformationRequest());
            var response = _connection.Read<ClientInformationResponse>();
            Ip = response.Ip;
        }
    }
}