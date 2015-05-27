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
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
            using (var stream = File.OpenWrite(filePath)) {
                _connection.Read(stream);
            }
        }
    }
}