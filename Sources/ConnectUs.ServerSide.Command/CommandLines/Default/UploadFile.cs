using System.IO;
using System.Linq;
using ConnectUs.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command.CommandLines.Default
{
    [CommandDescription(CommandName = "upload", Description = "Upload a file to the remote client.")]
    internal class UploadFile : CurrentClientCommand
    {
        public UploadFile(Context context)
            : base(context)
        {
        }

        protected override string HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var localFilePath = commandLine.Arguments.First(x => x.Name == "unknown");
            var remoteFolder = commandLine.Arguments.Last(x => x.Name == "unknown");
            if (localFilePath == null) {
                return "You should specify a local file path.";
            }
            if (remoteFolder == null) {
                return "You should specify a remote folder.";
            }
            var filePath = remoteClient.UploadFile(localFilePath.Value, remoteFolder.Value);
            return string.Format("The file '{0}' has been uploaded to the the remote location '{1}'.", Path.GetFileName(localFilePath.Value), filePath);
        }
    }
}