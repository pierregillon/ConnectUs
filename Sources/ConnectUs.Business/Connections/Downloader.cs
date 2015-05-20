using System.IO;

namespace ConnectUs.Business.Connections
{
    public class Downloader
    {
        private readonly IConnection _connection;

        public Downloader(IConnection connection)
        {
            _connection = connection;
        }

        public void Download(string filePath)
        {
            var fileContent = _connection.Read();
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
            File.AppendAllText(filePath, fileContent);
        }
    }
}