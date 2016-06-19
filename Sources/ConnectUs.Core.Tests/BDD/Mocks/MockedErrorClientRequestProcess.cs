using System.Text;
using Newtonsoft.Json;

namespace ConnectUs.Core.Tests.BDD.Mocks
{
    public class MockedErrorClientRequestProcess : MockedClientRequestProcess
    {
        public string ErrorMessage { get; set; }
        public MockedErrorClientRequestProcess(string message)
        {
            ErrorMessage = message;
        }
        public override byte[] Process(byte[] originalData)
        {
            var encoding = new UTF8Encoding();
            var json = "{\"Name\":\"ErrorResponse\", \"Message\":\"" + ErrorMessage + "\"}";
            return encoding.GetBytes(json);
        }
    }
}