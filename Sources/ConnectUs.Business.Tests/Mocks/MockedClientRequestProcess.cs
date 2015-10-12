using System.Collections.Generic;
using System.Text;
using ConnectUs.ClientSide;
using ConnectUs.Modules.Integrated.GetClientInformation;
using Newtonsoft.Json;

namespace ConnectUs.Business.Tests.Mocks
{
    public class MockedClientRequestProcess : IClientRequestProcessor
    {
        private readonly Dictionary<string, string> _processedData = new Dictionary<string, string>();
        private static readonly UTF8Encoding Encoding = new UTF8Encoding();

        public virtual byte[] Process(string requestName, byte[] originalData)
        {
            var json = Encoding.GetString(originalData);

            _processedData.Add(requestName, json);

            if (requestName == typeof (GetClientInformationRequest).Name) {
                var jsonResult = JsonConvert.SerializeObject(new GetClientInformationResponse
                {
                    Ip = "127.0.0.1",
                    MachineName = "my machine"
                });
                return Encoding.GetBytes(jsonResult);
            }
            return Encoding.GetBytes("{}");
        }
        public string GetData(string requestName)
        {
            return _processedData[requestName];
        }
    }
}