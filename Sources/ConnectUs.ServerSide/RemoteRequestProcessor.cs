using ConnectUs.Business;
using ConnectUs.Business.Connections;
using Newtonsoft.Json;

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
                    var jsonRequest = JsonConvert.SerializeObject(request);
                    _connection.Send(jsonRequest);
                    var jsonResponse = _connection.Read();
                    return JsonConvert.DeserializeObject<Response>(jsonResponse);
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