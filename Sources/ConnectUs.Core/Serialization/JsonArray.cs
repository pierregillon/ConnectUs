using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConnectUs.Core.Serialization
{
    public class JsonArray : IJsonObject
    {
        private readonly IList<IJsonObject> _jsonObjects = new List<IJsonObject>();

        public JsonArray(string json)
        {
            var elements = json.
                Substring(1, json.Length - 2)
                .Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();

            foreach (var element in elements) {
                _jsonObjects.Add(JsonObjectFactory.BuildJsonObject(element));
            }
        }
        public JsonArray(IEnumerable collection)
        {
            foreach (var element in collection) {
                _jsonObjects.Add(JsonObjectFactory.BuildJsonObject(element.GetType(), element));
            }
        }

        public object Materialize(Type type)
        {
            if (type.Implements(typeof(IList<>)) == false) {
                throw new NotImplementedException("Collection that does not implement IList<T> are not implemented yet.");
            }
            var argumentType = type.GetGenericArguments()[0];
            var collection = (IList) Activator.CreateInstance(type);
            foreach (var jsonObject in _jsonObjects) {
                collection.Add(jsonObject.Materialize(argumentType));
            }
            return collection;
        }

        public override string ToString()
        {
            return string.Join(",", _jsonObjects.Select(x => x.ToString())).Surround("[", "]");
        }
    }
}