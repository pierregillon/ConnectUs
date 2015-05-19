using System;

namespace ConnectUs.Common.GetClientInformation
{
    public class GetInformationCommand
    {
        public GetClientInformationResponse Execute(GetClientInformationRequest request)
        {
            return new GetClientInformationResponse
            {
                Ip = "195.23.65.25",
                MachineName = Environment.MachineName
            };
        }
    }
}