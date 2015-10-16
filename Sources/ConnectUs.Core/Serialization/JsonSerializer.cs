using System;

namespace ConnectUs.Core.Serialization
{
    public class JsonSerializer
    {
        public T Deserialize<T>(string json) where T : class
        {
            return (T) Deserialize(typeof (T), json);
        }
        public object Deserialize(Type type, string json)
        {
            if (string.IsNullOrEmpty(json) || json.Trim() == string.Empty) {
                throw new EmptyJsonException();
            }

            var jsonObject = JsonObjectFactory.BuildJsonObject(json);
            return jsonObject.Materialize(type);
        }

        public string Serialize<T>(T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var jsonObject = JsonObjectFactory.BuildJsonObject(typeof(T), obj);
            return jsonObject.ToString();
        }
        public string Serialize(object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var jsonObject = JsonObjectFactory.BuildJsonObject(obj.GetType(), obj);
            return jsonObject.ToString();
        }
    }
}