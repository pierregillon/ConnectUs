using System.IO;
using System.Linq;
using ConnectUs.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command.CommandLines.Default
{
    [CommandDescription(CommandName = "download", Description = "Download a remote file.")]
    internal class DownloadFile : CurrentClientCommand
    {
        public DownloadFile(Context context)
            : base(context)
        {
        }

        protected override string HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var remoteFilePath = commandLine.Arguments.First(x => x.Name == "unknown");
            var localFolder = commandLine.Arguments.Last(x => x.Name == "unknown");
            if (remoteFilePath == null) {
                return "You should specify a remote file path.";
            }
            if (localFolder == null) {
                return "You should specify a local folder.";
            }
            var filePath = remoteClient.DownloadFile(remoteFilePath.Value, localFolder.Value);
            return string.Format("The file '{0}' was successfully download at location '{1}'.", Path.GetFileName(remoteFilePath.Value), filePath);
        }
    }
}