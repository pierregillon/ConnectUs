using System;
using System.Linq;
using System.Net.NetworkInformation;
using ConnectUs.Modules.Integrated.GetClientInformation;

namespace ConnectUs.Modules.Integrated.GetFullClientInformation
{
    public class GetFullInformationCommand
    {
        public GetFullClientInformationResponse Execute(GetFullClientInformationRequest request)
        {
            return new GetFullClientInformationResponse
            {
                PublicIp = GetInformationCommand.PublicIp,
                MachineName = Environment.MachineName,
                OperatingSystem = Environment.OSVersion.VersionString,
                UserName = Environment.UserName,
                UserDomainName = Environment.UserDomainName,
                ProcessorCount = Environment.ProcessorCount,
                SystemStartedDate = DateTime.Now,
                StackTrace = Environment.StackTrace,
                SystemDirectory = Environment.SystemDirectory,
                NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces().Select(x =>
                {
                    return new NetworkInformation
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Addresses = x.GetIPProperties().UnicastAddresses.Select(add => add.Address.ToString())
                    };
                }).ToArray()
            };
        }
    }
}