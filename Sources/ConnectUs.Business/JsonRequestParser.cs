using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectUs.Business
{
    public class JsonRequestParser : IRequestParser
    {
        private const string RequestTypePropertyName = "Name";
        private static readonly UTF8Encoding Encoding = new UTF8Encoding();

        public string GetRequestName(byte[] data)
        {
            var json = Encoding.GetString(data);
            var jsonObject = JToken.ReadFrom(new JsonTextReader(new StringReader(json)));
            return jsonObject[RequestTypePropertyName].ToString();
        }
        public string GetError(byte[] data)
        {
            var json = Encoding.GetString(data);
            var request = JsonConvert.DeserializeObject<ErrorResponse>(json);
            return request.Error;
        }
        public object FromBytes(Type type, byte[] data)
        {
            var json = Encoding.GetString(data);
            return JsonConvert.DeserializeObject(json, type);
        }
        public T FromBytes<T>(byte[] data)
        {
            var json = Encoding.GetString(data);
            return JsonConvert.DeserializeObject<T>(json);
        }
        public byte[] ConvertToBytes(object request)
        {
            var jsonObject = JObject.FromObject(request);
            jsonObject.Add(RequestTypePropertyName, request.GetType().Name);
            var json = jsonObject.ToString(Formatting.None);
            return Encoding.GetBytes(json);
        }
    }
}