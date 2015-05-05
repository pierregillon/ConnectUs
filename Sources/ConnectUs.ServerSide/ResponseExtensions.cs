using ConnectUs.Business;
using ConnectUs.ClientSide;
using Newtonsoft.Json;

namespace ConnectUs.ServerSide
{
    public static class ResponseExtensions
    {
        public static T To<T>(this Response response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}