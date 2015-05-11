using System.Text;

namespace ConnectUs.Business.Encodings
{
    public class JsonEncoder : IEncoder
    {
        private readonly UTF8Encoding _encoding = new UTF8Encoding();

        public byte[] Encode(string data)
        {
            return _encoding.GetBytes(data);
        }
        public string Decode(byte[] encodedData)
        {
            return _encoding.GetString(encodedData);
        }
    }
}