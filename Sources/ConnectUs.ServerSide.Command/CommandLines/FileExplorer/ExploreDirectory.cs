using System;
using System.Linq;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Core.ServerSide.Requests;
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

        protected override void HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var directoryPath = commandLine.Arguments.FirstOrDefault(x => x.Name == "unknown");
            if (directoryPath == null) {
                WriteWarning("You should define the directory path.");
                return;
            }

            try {
                var request = new ExploreDirectoryRequest
                {
                    DirectoryPath = directoryPath.Value,
                    GetDirectories = true,
                    GetFiles = true
                };
                var response = remoteClient.Send<ExploreDirectoryRequest, ExploreDirectoryResponse>(request);
                WriteInfo(string.Join(Environment.NewLine, response.Files.Select(x => x.Name)));
            }
            catch (UnknownCommand) {
                WriteError("Unknown command 'dir'. The module 'ConnectUs.FileExplorer' seems to not be loaded.");
            }
        }
    }
}