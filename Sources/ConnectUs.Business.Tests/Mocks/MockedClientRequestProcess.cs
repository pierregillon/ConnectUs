using System.Collections.Generic;
using ConnectUs.ClientSide;
using ConnectUs.Common.GetClientInformation;
using Newtonsoft.Json;

namespace ConnectUs.Business.Tests.Mocks
{
    public class MockedClientRequestProcess : IClientRequestProcessor
    {
        private readonly Dictionary<string, string> _processedData = new Dictionary<string, string>();

        public virtual string Process(string requestName, string originalData)
        {
            _processedData.Add(requestName, originalData);

            if (requestName == typeof (GetClientInformationRequest).Name) {
                return JsonConvert.SerializeObject(new GetClientInformationResponse
                {
                    Ip = "127.0.0.1",
                    MachineName = "my machine"
                });
            }
            return "{}";
        }
        public string GetData(string requestName)
        {
            return _processedData[requestName];
        }
    }
}