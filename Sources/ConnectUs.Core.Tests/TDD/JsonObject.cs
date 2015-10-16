using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConnectUs.Core.Tests.TDD
{
    public class JsonObject : Dictionary<string, IMaterializable>, IMaterializable
    {
        private const string JsonPropertyName = @"'(?<name>[^:]*)'";
        private const string JsonPropertyValue = @"(?<value>[^,^}]*)";
        private const string JsonRegex = JsonPropertyName + @"\ *:\ *" + JsonPropertyValue;
        private static readonly Regex PropertyRegex = new Regex(JsonRegex.Replace("'", "\""));
        private static readonly Regex ClassRegex = new Regex(@"(?<name>'[^,]*?'):(?<value>[{\[].*?[}\]])".Replace("'", "\""));

        private JsonObject()
        {
        }
        private JsonObject(object obj)
        {
            foreach (var property in GetPropertiesToSerialize(obj)) {
                this[property.Name.Surround("\"")] = GetValue(property, obj);
            }
        }

        // ----- Public methods
        public object Materialize(Type type)
        {
            var instance = Activator.CreateInstance(type);
            foreach (var keyValue in this) {
                var propertyInfo = Key(type, keyValue.Key);
                if (propertyInfo != null) {
                    propertyInfo.SetValue(instance, keyValue.Value.Materialize(propertyInfo.PropertyType));
                }
            }
            return instance;
        }
        private static PropertyInfo Key(Type type, string propertyName)
        {
            if (propertyName.IsSurroundBy('\"')) {
                propertyName = propertyName.Substring(1, propertyName.Length - 2);
            }
            return type.GetProperty(propertyName);
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

            json = ParseSubObjects(jsonObject, json);
            ParseProperties(jsonObject, json);

            return jsonObject;
        }
        public static JsonObject From(object origin)
        {
            return new JsonObject(origin);
        }

        // ----- Utils
        private static void ParseProperties(JsonObject jsonObject, string json)
        {
            var matches = PropertyRegex.Matches(json);
            foreach (Match match in matches) {
                var name = match.Groups["name"].Value;
                var value = match.Groups["value"].Value.Trim();

                if (value.IsSurroundBy('\"')) {
                    jsonObject.Add(name, new StringJsonProperty(value));
                }
                else {
                    jsonObject.Add(name, new NumericJsonProperty(value));
                }
            }
        }
        private static string ParseSubObjects(JsonObject jsonObject, string json)
        {
            var classMatches = ClassRegex.Matches(json);
            foreach (Match classMatch in classMatches) {
                var name = classMatch.Groups["name"].Value;
                var value = classMatch.Groups["value"].Value;
                jsonObject[name] = Parse(value);
                json = json.Replace(classMatch.Value, "");
            }
            return json;
        }
        private static IEnumerable<PropertyInfo> GetPropertiesToSerialize(object origin)
        {
            return origin
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);
        }
        private static IMaterializable GetValue(PropertyInfo property, object instance)
        {
            if (property.PropertyType.IsNumeric()) {
                return new NumericJsonProperty(property.GetValue(instance).ToString());
            }
            if (property.PropertyType == typeof (string)) {
                return new StringJsonProperty(property.GetValue(instance).ToString().Surround("\""));
            }
            return new JsonObject(property.GetValue(instance));
        }
    }
}