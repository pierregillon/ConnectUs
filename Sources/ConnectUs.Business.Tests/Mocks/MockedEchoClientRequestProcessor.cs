using System;
using ConnectUs.ClientSide;
using Newtonsoft.Json;

namespace ConnectUs.Business.Tests.Mocks
{
    public class MockedEchoClientRequestProcessor : IClientRequestProcessor
    {
        public object Process(string requestName, string originalData)
        {
            if (requestName == "Echo") {
                var request = JsonConvert.DeserializeObject<EchoRequest>(originalData);
                return new EchoResponse{Result =  request.Value};
            }
            throw new Exception("invalid requestname");
        }
    }

    public class EchoResponse : Response
    {
        public string Result { get; set; }
    }

    public class EchoRequest : Request
    {
        public string Value { get; set; }
        public EchoRequest(string value)
            : base("Echo")
        {
            Value = value;
        }
    }
}