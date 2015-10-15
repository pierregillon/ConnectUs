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
            return Materialize(type, jsonObject);
        }
        public static string Serialize(object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var jsonObject = JsonObject.From(obj);
            return jsonObject.ToString();
        }

        private static object Materialize(Type type, JsonObject jsonObject)
        {
            var instance = Activator.CreateInstance(type);
            var properties = jsonObject.GetProperties();
            foreach (var property in properties) {
                property.SetTo(instance);
            }
            return instance;
        }
    }

    public class JsonObject : Dictionary<string, string>
    {
        private const string JsonPropertyName = @"'(?<name>[^:]*)'";
        private const string JsonPropertyValue = @"'?(?<value>[^,^}^']*)'?";
        private const string JsonRegex = JsonPropertyName + @"\ *:\ *" + JsonPropertyValue;
        private static readonly Regex Regex = new Regex(JsonRegex.Replace("'", "\""));

        private readonly string _json;

        public JsonObject()
        {
        }
        private JsonObject(string json)
        {
            _json = json;
        }

        public IEnumerable<JsonProperty> GetProperties()
        {
            var matches = Regex.Matches(_json);
            foreach (Match match in matches) {
                var name = match.Groups["name"].Value;
                var value = match.Groups["value"].Value;
                yield return new JsonProperty(name, value);
            }
        }

        public override string ToString()
        {
            var elements = this.Select(x => x.Key + ":" + x.Value).ToArray();
            return string.Join(",", elements).Surround("{", "}");
        }

        public static JsonObject Parse(string json)
        {
            return new JsonObject(json);
        }
        public static JsonObject From(object o)
        {
            var jsonObject = new JsonObject();
            foreach (var property in o.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty)) {
                jsonObject[property.Name.Surround("\"")] = GetValue(property, o);
            }
            return jsonObject;
        }
        private static string GetValue(PropertyInfo property, object o)
        {
            if (property.PropertyType.IsPrimitive) {
                if (property.PropertyType.IsNumeric()) {
                    return property.GetValue(o).ToString();
                }
            }
            if (property.PropertyType == typeof (string)) {
                return property.GetValue(o).ToString().Surround("\"");
            }

            throw new NotImplementedException();
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