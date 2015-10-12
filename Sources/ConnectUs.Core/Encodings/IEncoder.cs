namespace ConnectUs.Core.Encodings
{
    public interface IEncoder
    {
        byte[] Encode(string data);
        string Decode(byte[] encodedData);
    }
}