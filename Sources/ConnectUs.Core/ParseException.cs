using System;

namespace ConnectUs.Core
{
    public class ParseException : Exception
    {
        public ParseException(string message) :base(message)
        {
            
        }
    }
}