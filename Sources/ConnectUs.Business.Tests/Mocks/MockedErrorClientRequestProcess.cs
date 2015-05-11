namespace ConnectUs.Business.Tests.Mocks
{
    public class MockedErrorClientRequestProcess : MockedClientRequestProcess
    {
        public string ErrorMessage { get; set; }
        public MockedErrorClientRequestProcess(string message)
        {
            ErrorMessage = message;
        }
        public override object Process(string requestName, string originalData)
        {
            return new ErrorResponse
            {
                Error = ErrorMessage
            };
        }
    }
}