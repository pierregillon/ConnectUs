using System;
using System.IO;

namespace ConnectUs.Business.Connections
{
    public static class StreamExtensions
    {
        public static void ForeachRead(this Stream stream, int bufferSize, Action<byte[]> callback)
        {
            for (long currentByteSentCount = 0; currentByteSentCount < stream.Length;) {
                var size = stream.Length - currentByteSentCount > bufferSize ? bufferSize : stream.Length - currentByteSentCount;
                var buffer = new byte[size];
                stream.Read(buffer, 0, (int) size);
                callback(buffer);
                currentByteSentCount += size;
            }
        }
    }
}