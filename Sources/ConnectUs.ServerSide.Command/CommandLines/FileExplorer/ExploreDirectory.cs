using System;
using System.Linq;
using ConnectUs.FileExplorer;

namespace ConnectUs.ServerSide.Command.CommandLines.FileExplorer
{
    [CommandDescription(CommandName = "dir", Description = "Display files and directories of a remote folder.", Category = "FileExplorer")]
    internal class ExploreDirectory : CurrentClientCommand
    {
        public ExploreDirectory(Context context)
            : base(context)
        {
        }

        protected override string HandleInternal(CommandLine commandLine, RemoteClient remoteClient)
        {
            var directoryPath = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (directoryPath == null) {
                return "You should define the directory path.";
            }

            var request = new ExploreDirectoryRequest
            {
                DirectoryPath = directoryPath.Value,
                GetDirectories = true,
                GetFiles = true
            };
            var response = remoteClient.ExecuteCommand<ExploreDirectoryRequest, ExploreDirectoryResponse>(request);
            return string.Join(Environment.NewLine, response.Files.Select(x => x.Name));
        }
    }
}