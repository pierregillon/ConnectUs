using System.Collections.Generic;

namespace ConnectUs.FileExplorer
{
    public class Module
    {
        public IEnumerable<object> GetCommands()
        {
            return new object[]
            {
                new ExploreDirectoryCommand()
            };
        }
    }
}
