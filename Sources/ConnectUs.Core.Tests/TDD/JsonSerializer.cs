using System;
using System.Collections.Generic;
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

    public class JsonObject
    {
        private const string JsonPropertyName = @"'(?<name>[^:]*)'";
        private const string JsonPropertyValue = @"'?(?<value>[^,^}^']*)'?";
        private const string JsonRegex = JsonPropertyName + @"\ *:\ *" + JsonPropertyValue;
        private static readonly Regex Regex = new Regex(JsonRegex.Replace("'", "\""));

        private readonly string _json;

        private JsonObject(string json)
        {
            _json = json;
        }
        public static JsonObject Parse(string json)
        {
            return new JsonObject(json);
        }

        public IEnumerable<JsonProperty> GetProperties()
        {
            var matches = Regex.Matches(_json);
            foreach (Match match in matches)
            {
                var name = match.Groups["name"].Value;
                var value = match.Groups["value"].Value;
                yield return new JsonProperty(name, value);
            }
        }
    }
}