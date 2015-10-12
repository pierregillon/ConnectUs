using System;
using System.Collections.Generic;
using System.Linq;
using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.FileExplorer;

namespace ConnectUs.ServerSide.Application.CommandLines
{
    public class ExploreDirectoryCommandLine : ICommandLine
    {
        public string Name { get { return "dir"; } }
        public string ExecuteCommand(IRemoteClient remoteClient, IEnumerable<string> parameters)
        {
            var request = new ExploreDirectoryRequest
            {
                DirectoryPath = parameters.First(),
                GetDirectories = true,
                GetFiles = true
            };
            var response = remoteClient.Send<ExploreDirectoryRequest, ExploreDirectoryResponse>(request);
            return string.Join(Environment.NewLine, response.Files.Select(x => x.Name));
        }
    }
}