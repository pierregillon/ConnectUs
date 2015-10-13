using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConnectUs.Core.Tests.TDD
{
    public class JsonSerializer
    {
        private const string JsonPropertyName = @"'(?<name>[^:]*)'";
        private const string JsonPropertyValue = @"'?(?<value>[^,^}^']*)'?";
        private const string JsonRegex = JsonPropertyName + @"\ *:\ *" + JsonPropertyValue;
        private static readonly Regex Regex = new Regex(JsonRegex.Replace("'", "\""));

        public static object Deserialize(Type type, string json)
        {
            if (string.IsNullOrEmpty(json) || json.Trim() == string.Empty) {
                throw new EmptyJsonException();
            }
            var instance = Activator.CreateInstance(type);
            var properties = GetProperties(json);
            foreach (var property in properties) {
                property.SetTo(instance);
            }
            return instance;
        }

        private static IEnumerable<JsonProperty> GetProperties(string json)
        {
            var matches = Regex.Matches(json);
            foreach (Match match in matches) {
                var name = match.Groups["name"].Value;
                var value = match.Groups["value"].Value;
                yield return new JsonProperty(name, value);
            }
        }
    }
}