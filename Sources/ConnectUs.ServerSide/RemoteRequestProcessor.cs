using ConnectUs.Business;
using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide
{
    public class RemoteRequestProcessor : IRequestProcessor
    {
        private readonly IConnection _connection;
        private readonly object _locker = new object();

        // ----- Constructors
        public RemoteRequestProcessor(IConnection connection)
        {
            _connection = connection;
        }

        // ----- Public methods
        public Response Process(Request request)
        {
            try {
                lock (_locker) {
                    _connection.Send(request);
                    return _connection.Read<Response>();
                }
            }
            catch (ConnectionException) {
                _connection.Close();
                throw;
            }
        }
        public void Close()
        {
            _connection.Close();
        }
    }
}