using System;
using System.Threading;

namespace ConnectUs.ClientSide.Commands.GetClientInformation
{
    public class GetInformationCommand
    {
        public static string PublicIp { get; set; }

        static GetInformationCommand()
        {
            new Thread(() => PublicIp = NetHelper.GetPublicIp()).Start();
        }

        public GetClientInformationResponse Execute(GetClientInformationRequest request)
        {
            return new GetClientInformationResponse
            {
                Ip = PublicIp,
                MachineName = Environment.MachineName,
                OperatingSystem = Environment.OSVersion.Platform.ToString(),
                UserName = Environment.UserName,
            };
        }
    }
}