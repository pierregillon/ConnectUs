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
            

            DownloadInternal(filePath);
        }

        private void DownloadInternal(string filePath)
        {
            _connection.Send(new byte[1]);
            using (var stream = File.OpenWrite(filePath)) {
                _connection.Read(stream);
            }
        }
    }
}