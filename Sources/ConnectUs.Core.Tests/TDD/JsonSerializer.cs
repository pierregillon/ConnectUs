using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConnectUs.Core.Tests.TDD
{
    public class JsonSerializer
    {
        public static object Deserialize(Type type, string json)
        {
            if (string.IsNullOrEmpty(json) || json.Trim() == string.Empty) {
                throw new EmptyJsonException();
            }

            var jsonObject = JsonObject.Parse(json);
            return jsonObject.Materialize(type);
        }
        public static string Serialize(object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var jsonObject = JsonObject.From(obj);
            return jsonObject.ToString();
        }
    }

    public class JsonObject : Dictionary<string, object>
    {
        private const string JsonPropertyName = @"'(?<name>[^:]*)'";
        private const string JsonPropertyValue = @"'?(?<value>[^,^}^']*)'?";
        private const string JsonRegex = JsonPropertyName + @"\ *:\ *" + JsonPropertyValue;
        private static readonly Regex Regex = new Regex(JsonRegex.Replace("'", "\""));

        private JsonObject()
        {
        }

        // ----- Public methods
        public object Materialize(Type type)
        {
            var instance = Activator.CreateInstance(type);
            foreach (var keyValue in this) {
                var property = new JsonProperty(keyValue.Key, keyValue.Value.ToString());
                property.SetTo(instance);
            }
            return instance;
        }

        // ----- Overrides
        public override string ToString()
        {
            var elements = this.Select(x => x.Key + ":" + x.Value).ToArray();
            return string.Join(",", elements).Surround("{", "}");
        }

        // ----- Statics
        public static JsonObject Parse(string json)
        {
            var jsonObject = new JsonObject();
            var matches = Regex.Matches(json);
            foreach (Match match in matches) {
                var name = match.Groups["name"].Value;
                var value = match.Groups["value"].Value;
                jsonObject.Add(name, value);
            }
            return jsonObject;
        }
        public static JsonObject From(object origin)
        {
            return GetJsonObject(origin);
        }
        private static JsonObject GetJsonObject(object origin)
        {
            var jsonObject = new JsonObject();
            foreach (var property in GetPropertiesToSerialize(origin)) {
                jsonObject[property.Name.Surround("\"")] = GetValue(property, origin);
            }
            return jsonObject;
        }
        private static IEnumerable<PropertyInfo> GetPropertiesToSerialize(object origin)
        {
            return origin
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);
        }
        private static object GetValue(PropertyInfo property, object o)
        {
            if (property.PropertyType.IsPrimitive) {
                if (property.PropertyType.IsNumeric()) {
                    return property.GetValue(o).ToString();
                }
            }
            if (property.PropertyType == typeof (string)) {
                return property.GetValue(o).ToString().Surround("\"");
            }

            return GetJsonObject(property.GetValue(o));
        }
    }

    public static class StringExtensions
    {
        public static string Surround(this string value, string element)
        {
            return string.Format("{0}{1}{2}", element, value, element);
        }

        public static string Surround(this string value, string start, string end)
        {
            return string.Format("{0}{1}{2}", start, value, end);
        }
    }

    public static class TypeExtensions
    {
        public static bool IsNumeric(this Type type)
        {
            switch (Type.GetTypeCode(type)) {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>)) {
                        return Nullable.GetUnderlyingType(type).IsNumeric();
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}