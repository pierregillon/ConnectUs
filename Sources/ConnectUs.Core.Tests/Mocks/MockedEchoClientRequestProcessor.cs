using System;
using System.Text;
using ConnectUs.Core.ClientSide;
using Newtonsoft.Json;

namespace ConnectUs.Core.Tests.Mocks
{
    public class MockedEchoClientRequestProcessor : IClientRequestProcessor
    {
        public byte[] Process(string requestName, byte[] data)
        {
            if (requestName == typeof (EchoRequest).Name) {
                var encoding = new UTF8Encoding();
                var request = JsonConvert.DeserializeObject<EchoRequest>(encoding.GetString(data));
                var json = JsonConvert.SerializeObject(new EchoResponse {Result = request.Value});
                return encoding.GetBytes(json);
            }
            throw new Exception("invalid requestname");
        }
    }
}