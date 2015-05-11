﻿using System.Collections.Generic;
using ConnectUs.ClientSide;

namespace ConnectUs.Business.Tests.Mocks
{
    public class MockedClientRequestProcess : IClientRequestProcessor
    {
        private readonly Dictionary<string, string> _processedData = new Dictionary<string, string>(); 

        public virtual object Process(string requestName, string originalData)
        {
            _processedData.Add(requestName, originalData);

            if (requestName == "GetClientInformation") {
                return new GetClientInformationResponse
                {
                    Ip = "127.0.0.1",
                    MachineName = "my machine"
                };
            }
            return new object();
        }
        public string GetData(string requestName)
        {
            return _processedData[requestName];
        }
    }
}