using System;

namespace ConnectUs.Core.Connections
{
    public class NoDataToReadFromConnectionException : ConnectionException
    {
        public NoDataToReadFromConnectionException(string message) : base(message) {}
        public NoDataToReadFromConnectionException(string message, Exception innerException) : base(message, innerException) {}
    }
}