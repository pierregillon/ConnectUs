using System;

namespace ConnectUs.Business.Connections
{
    public class NoDataToReadFromConnectionException : ConnectionException
    {
        public NoDataToReadFromConnectionException(string message, Exception innerException) : base(message, innerException) {}
    }
}