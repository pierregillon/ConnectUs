using System.IO;

namespace ConnectUs.Core.Connections
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
            RemoveFileIfExists(filePath);

            _connection.Send(new byte[1]);
            using (var stream = File.OpenWrite(filePath)) {
                _connection.Read(stream);
            }
        }

        private static void RemoveFileIfExists(string filePath)
        {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
        }
    }
}