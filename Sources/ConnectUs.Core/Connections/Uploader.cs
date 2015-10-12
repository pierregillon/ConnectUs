using System.IO;

namespace ConnectUs.Core.Connections
{
    public class Uploader
    {
        private readonly IConnection _connection;

        public Uploader(IConnection connection)
        {
            _connection = connection;
        }

        public void Upload(string filePath)
        {
            _connection.Read();
            using (var stream = File.OpenRead(filePath)) {
                _connection.Send(stream);
            }
        }
    }
}