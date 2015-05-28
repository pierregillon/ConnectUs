using ConnectUs.Business.Connections;

namespace ConnectUs.ServerSide
{
    public class ServerFileCommunicator : IServerFileCommunicator
    {
        private readonly IConnection _connection;

        public ServerFileCommunicator(IConnection connection)
        {
            _connection = connection;
        }

        public void Upload(string filePath)
        {
            var uploader = new Uploader(_connection);
            uploader.Upload(filePath);
        }
    }
}