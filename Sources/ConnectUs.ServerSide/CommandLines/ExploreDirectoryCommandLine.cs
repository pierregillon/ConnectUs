using System;
using System.Collections.Generic;
using System.Linq;
using ConnectUs.FileExplorer;

namespace ConnectUs.ServerSide.CommandLines
{
    public class ExploreDirectoryCommandLine : ICommandLine
    {
        public string Name { get { return "dir"; } }
        public string ExecuteCommand(Client client, IEnumerable<string> parameters)
        {
            var request = new ExploreDirectoryRequest
            {
                DirectoryPath = parameters.First(),
                GetDirectories = true,
                GetFiles = true
            };
            var response = client.ExecuteCommand<ExploreDirectoryRequest, ExploreDirectoryResponse>(request);
            return string.Join(Environment.NewLine, response.Files.Select(x => x.Name));
        }
    }
}