using System;

namespace ConnectUs.Business
{
    public interface IRequestParser
    {
        string GetRequestName(string data);
        string GetError(string data);
        string ConvertToString(object request);
        T FromString<T>(string text);
        object FromString(string text, Type type);
    }
}