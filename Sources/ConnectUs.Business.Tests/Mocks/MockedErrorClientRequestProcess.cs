﻿using System.Text;
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
        public override byte[] Process(string requestName, byte[] originalData)
        {
            var encoding = new UTF8Encoding();
            var json = JsonConvert.SerializeObject(new ErrorResponse
            {
                Error = ErrorMessage
            });
            return encoding.GetBytes(json);
        }
    }
}