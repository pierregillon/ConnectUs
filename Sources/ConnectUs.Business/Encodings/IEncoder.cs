namespace ConnectUs.Business.Encodings
{
    public interface IEncoder
    {
        byte[] Encode(string data);
        string Decode(byte[] encodedData);
    }
}