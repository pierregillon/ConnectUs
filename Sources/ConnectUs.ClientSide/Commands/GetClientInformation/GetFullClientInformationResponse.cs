using System;
using System.Collections.Generic;

namespace ConnectUs.ClientSide.Commands.GetClientInformation
{
    public class GetFullClientInformationResponse {
        public string PublicIp { get; set; }
        public string MachineName { get; set; }
        public string OperatingSystem { get; set; }
        public string UserName { get; set; }
        public string UserDomainName { get; set; }
        public int ProcessorCount { get; set; }
        public DateTime SystemStartedDate { get; set; }
        public string StackTrace { get; set; }
        public string SystemDirectory { get; set; }
        public IEnumerable<NetworkInformation> NetworkInterfaces { get; set; }
    }
}