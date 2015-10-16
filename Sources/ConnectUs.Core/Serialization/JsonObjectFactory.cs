using System;
using System.Collections;

namespace ConnectUs.Core.Serialization
{
    internal class JsonObjectFactory
    {
        public static IJsonObject BuildJsonObject(string json)
        {
            if (json.IsSurroundBy('{', '}')) {
                var builder = new JsonClassBuilder();
                return builder.Build(json);
            }
            if (json.IsSurroundBy('[', ']')) {
                return new JsonArray(json);
            }
            if (json.IsSurroundBy('\"')) {
                return new StringJsonProperty(json);
            }
            return new NumericJsonProperty(json);
        }

        public static IJsonObject BuildJsonObject(Type type, object instance)
        {
            if (type.IsNumeric()) {
                return new NumericJsonProperty(instance);
            }
            if (type == typeof (string)) {
                return new StringJsonProperty(instance);
            }
            if (type.GetInterface("IEnumerable") != null) {
                return new JsonArray((IEnumerable)instance);
            }
            return new JsonClass(instance);
        }
    }
}