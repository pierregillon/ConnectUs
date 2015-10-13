using System.Text;
using ConnectUs.Core.ClientSide;
using Newtonsoft.Json;

namespace ConnectUs.Core.Tests.BDD.Mocks
{
    public class MockedEchoClientRequestProcessor : IClientRequestProcessor
    {
        public byte[] Process(byte[] data)
        {
            var encoding = new UTF8Encoding();
            var request = JsonConvert.DeserializeObject<EchoRequest>(encoding.GetString(data));
            var json = JsonConvert.SerializeObject(new EchoResponse {Result = request.Value});
            return encoding.GetBytes(json);
        }
    }
}