using System.IO;

namespace ConnectUs.Business.Connections
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
            var fileContent = File.ReadAllText(filePath);
            _connection.Send(fileContent);
        }
    }
}