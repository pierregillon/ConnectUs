using Newtonsoft.Json;

namespace ConnectUs.Business
{
    public class JsonRequestParser : IRequestParser
    {
        public string GetRequestName(string data)
        {
            var request = JsonConvert.DeserializeObject<RequestBase>(data);
            return request.Name;
        }
        public string GetError(string data)
        {
            var request = JsonConvert.DeserializeObject<ErrorResponse>(data);
            return request.Error;
        }
    }
}