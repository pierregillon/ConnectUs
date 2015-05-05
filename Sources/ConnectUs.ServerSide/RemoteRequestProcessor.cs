using ConnectUs.Business;
using ConnectUs.ClientSide;

namespace ConnectUs.ServerSide
{
    public class RemoteRequestProcessor : IRequestProcessor
    {
        private readonly IConnection _connection;

        public RemoteRequestProcessor(IConnection connection)
        {
            _connection = connection;
        }

        public Response Process(Request request)
        {
            _connection.Send(request);
            return _connection.Read();
        }
    }
}
