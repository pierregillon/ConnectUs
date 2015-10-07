﻿using System;

namespace ConnectUs.ClientSide.Commands.GetClientInformation
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