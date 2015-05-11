using System;

namespace ConnectUs.Business.Commands.ClientInformation
{
    public class GetInformationCommand : CommandBase<GetClientInformationRequest, GetClientInformationResponse>
    {
        protected override GetClientInformationResponse ExecuteRequest(GetClientInformationRequest request)
        {
            return new GetClientInformationResponse
            {
                Ip = "195.23.65.25",
                MachineName = Environment.MachineName
            };
        }
    }
}