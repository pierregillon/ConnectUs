using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConnectUs.Business
{
    public class JsonRequestParser : IRequestParser
    {
        private const string RequestTypePropertyName = "Name";

        public string GetRequestName(string data)
        {
            var jsonObject = JToken.ReadFrom(new JsonTextReader(new StringReader(data)));
            return jsonObject[RequestTypePropertyName].ToString();
        }
        public string GetError(string data)
        {
            var request = JsonConvert.DeserializeObject<ErrorResponse>(data);
            return request.Error;
        }
        public string ConvertToString(object request)
        {
            var jsonObject = JObject.FromObject(request);
            jsonObject.Add(RequestTypePropertyName, request.GetType().Name);
            return jsonObject.ToString(Formatting.None);
        }
        public T FromString<T>(string text)
        {
            return JsonConvert.DeserializeObject<T>(text);
        }
        public object FromString(string text, Type type)
        {
            return JsonConvert.DeserializeObject(text, type);
        }
    }
}