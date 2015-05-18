using System.Collections.Generic;

namespace ConnectUs.FileExplorer
{
    public class ExploreDirectoryResponse
    {
        public IEnumerable<ElementResponse> Files { get; set; }
        public IEnumerable<ElementResponse> Directories { get; set; }
    }
}