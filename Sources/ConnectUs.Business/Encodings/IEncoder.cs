namespace ConnectUs.Business.Encodings
{
    public interface IEncoder
    {
        byte[] Encode<T>(T request);
        T Decode<T>(byte[] buffer);
    }
}