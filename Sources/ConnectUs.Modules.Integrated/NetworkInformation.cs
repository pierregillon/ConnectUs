using System.Collections.Generic;

namespace ConnectUs.Modules.Integrated
{
    public class NetworkInformation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Addresses { get; set; }
    }
}