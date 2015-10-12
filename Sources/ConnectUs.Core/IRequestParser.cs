using System;

namespace ConnectUs.Core
{
    public interface IRequestParser
    {
        string GetRequestName(byte[] data);
        string GetError(byte[] data);

        object FromBytes(Type type, byte[] data);
        T FromBytes<T>(byte[] data);
        byte[] ConvertToBytes(object request);
    }
}