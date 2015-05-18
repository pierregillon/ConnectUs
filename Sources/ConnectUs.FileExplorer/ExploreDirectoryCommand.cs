using System.IO;
using System.Linq;

namespace ConnectUs.FileExplorer
{
    public class ExploreDirectoryCommand
    {
        public ExploreDirectoryResponse Execute(ExploreDirectoryRequest request)
        {
            var response = new ExploreDirectoryResponse();
            if (request.GetDirectories) {
                response.Directories = Directory.GetDirectories(request.DirectoryPath).Select(x => new ElementResponse {Name = x});
            }
            if (request.GetFiles) {
                response.Files = Directory.GetFiles(request.DirectoryPath).Select(x => new ElementResponse {Name = x});
            }
            return response;
        }
    }
}