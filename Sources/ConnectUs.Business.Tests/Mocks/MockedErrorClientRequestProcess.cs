using Newtonsoft.Json;

namespace ConnectUs.Business.Tests.Mocks
{
    public class MockedErrorClientRequestProcess : MockedClientRequestProcess
    {
        public string ErrorMessage { get; set; }
        public MockedErrorClientRequestProcess(string message)
        {
            ErrorMessage = message;
        }
        public override string Process(string requestName, string originalData)
        {
            return JsonConvert.SerializeObject(new ErrorResponse
            {
                Error = ErrorMessage
            });
        }
    }
}