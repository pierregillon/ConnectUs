using System;

namespace ConnectUs.Core
{
    public class ErrorResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }

        public ErrorResponse()
        {
        }
        public ErrorResponse(Exception exception, int code=-1)
        {
            Message = exception.Message;
            Code = code;
        }
    }
}