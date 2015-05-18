﻿using System;
using ConnectUs.ClientSide;
using Newtonsoft.Json;

namespace ConnectUs.Business.Tests.Mocks
{
    public class MockedEchoClientRequestProcessor : IClientRequestProcessor
    {
        public string Process(string requestName, string originalData)
        {
            if (requestName == typeof (EchoRequest).Name) {
                var request = JsonConvert.DeserializeObject<EchoRequest>(originalData);
                return JsonConvert.SerializeObject(new EchoResponse {Result = request.Value});
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
        {
            Value = value;
        }
    }
}