using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConnectUs.Core.ClientSide;
using ConnectUs.Modules.Integrated.GetClientInformation;
using Newtonsoft.Json;

namespace ConnectUs.Core.Tests.BDD.Mocks
{
    public class MockedClientRequestProcess : IClientRequestProcessor
    {
        private readonly List<string> _processedData = new List<string>();
        private static readonly UTF8Encoding Encoding = new UTF8Encoding();

        public virtual byte[] Process(byte[] originalData)
        {
            var json = Encoding.GetString(originalData);

            _processedData.Add(json);

            var jsonResult = JsonConvert.SerializeObject(new GetClientInformationResponse
            {
                Ip = "127.0.0.1",
                MachineName = "my machine"
            });
            return Encoding.GetBytes(jsonResult);
        }
        public string GetData()
        {
            return _processedData.Last();
        }
    }
}