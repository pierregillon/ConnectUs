using System;

namespace ConnectUs.Core.Serialization
{
    public class JsonSerializer
    {
        public static object Deserialize(Type type, string json)
        {
            if (string.IsNullOrEmpty(json) || json.Trim() == string.Empty) {
                throw new EmptyJsonException();
            }

            var jsonObject = JsonObjectFactory.BuildJsonObject(json);
            return jsonObject.Materialize(type);
        }
        public static string Serialize(object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var jsonObject = JsonObjectFactory.BuildJsonObject(obj.GetType(), obj);
            return jsonObject.ToString();
        }
    }
}