namespace ConnectUs.Core.Tests.BDD.Mocks
{
    public class EchoRequest
    {
        public string Value { get; set; }
        public EchoRequest(string value)
        {
            Value = value;
        }
    }
}