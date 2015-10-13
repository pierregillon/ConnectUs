using System.IO;
using System.Linq;
using ConnectUs.Core.ServerSide.Clients;

namespace ConnectUs.ServerSide.Command.CommandLines.Default
{
    [CommandDescription(CommandName = "download", Description = "Download a remote file.")]
    internal class DownloadFile : CurrentClientCommand
    {
        public DownloadFile(Context context)
            : base(context)
        {
        }

        protected override void HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var remoteFilePath = commandLine.Arguments.First(x => x.Name == "unknown");
            var localFolder = commandLine.Arguments.Last(x => x.Name == "unknown");
            if (remoteFilePath == null) {
                WriteWarning("You should specify a remote file path.");
                return;
            }
            if (localFolder == null) {
                WriteWarning("You should specify a local folder.");
                return;
            }
            var filePath = remoteClient.DownloadFile(remoteFilePath.Value, localFolder.Value);
            WriteInfo("The file '{0}' was successfully download at location '{1}'.", Path.GetFileName(remoteFilePath.Value), filePath);
        }
    }
}