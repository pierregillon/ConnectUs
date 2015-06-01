using System.IO;
using ConnectUs.Business.Connections;

namespace ConnectUs.ClientSide.Commands.Upload
{
    public class UploadCommand
    {
        private readonly IClientInformation _clientInformation;

        public UploadCommand(IClientInformation clientInformation)
        {
            _clientInformation = clientInformation;
        }

        // ----- Public methods
        public UploadResponse Execute(UploadRequest request)
        {
            CheckDirectory(request);

            var downloader = new Downloader(_clientInformation.CurrentConnection);
            downloader.Download(request.FilePath);

            return new UploadResponse
            {
                FilePath = GetFullFilePath(request)
            };
        }

        // ----- Utils
        private static string GetFullFilePath(UploadRequest request)
        {
            if (Path.IsPathRooted(request.FilePath) == false) {
                return Path.Combine(Directory.GetCurrentDirectory(), request.FilePath);
            }
            return request.FilePath;
        }
        private static void CheckDirectory(UploadRequest request)
        {
            var directoryName = Path.GetDirectoryName(request.FilePath);
            if (string.IsNullOrEmpty(directoryName) == false) {
                if (Directory.Exists(directoryName) == false) {
                    Directory.CreateDirectory(directoryName);
                }
            }
        }
    }
}