using System.Text;
using Newtonsoft.Json;

namespace ConnectUs.Business.Encodings
{
    public class JsonEncoder : IEncoder
    {
        private readonly UTF8Encoding _encoding = new UTF8Encoding();

        public byte[] Encode<T>(T request)
        {
            var json = JsonConvert.SerializeObject(request);
            return _encoding.GetBytes(json);
        }

        public T Decode<T>(byte[] buffer)
        {
            var json = _encoding.GetString(buffer);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}