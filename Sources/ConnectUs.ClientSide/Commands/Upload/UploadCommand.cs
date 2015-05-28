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

        public UploadResponse Execute(UploadRequest request)
        {
            var downloader = new Downloader(_clientInformation.CurrentConnection);
            downloader.Download(Path.Combine(@"c:\TEMP\", request.FileName));
            return new UploadResponse();
        }
    }
}
