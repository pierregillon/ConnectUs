using System;
using System.IO;
using ConnectUs.Business.Connections;
using ConnectUs.Modules.Integrated.FileTransfert;

namespace ConnectUs.ClientSide.Commands.Upload
{
    internal class DownloadCommand
    {
        private readonly IClientInformation _clientInformation;

        public DownloadCommand(IClientInformation clientInformation)
        {
            _clientInformation = clientInformation;
        }

        // ----- Public methods
        public DownloadResponse Execute(DownloadRequest request)
        {
            CheckFile(request.FilePath);

            var uploader = new Uploader(_clientInformation.CurrentConnection);
            uploader.Upload(request.FilePath);

            return new DownloadResponse
            {
                RemoteFilePath = GetFullFilePath(request)
            };
        }

        // ----- Utils
        private static void CheckFile(string filePath)
        {
            if (File.Exists(filePath) == false) {
                throw new Exception(string.Format("The file '{0}' was not found.", filePath));
            }
        }
        private static string GetFullFilePath(DownloadRequest request)
        {
            if (Path.IsPathRooted(request.FilePath) == false) {
                return Path.Combine(Directory.GetCurrentDirectory(), request.FilePath);
            }
            return request.FilePath;
        }
    }
}