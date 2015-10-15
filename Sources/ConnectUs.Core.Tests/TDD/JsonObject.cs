using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConnectUs.Core.Tests.TDD
{
    public class JsonObject : Dictionary<string, object>
    {
        private const string JsonPropertyName = @"'(?<name>[^:]*)'";
        private const string JsonPropertyValue = @"'?(?<value>[^,^}^']*)'?";
        private const string JsonRegex = JsonPropertyName + @"\ *:\ *" + JsonPropertyValue;
        private static readonly Regex PropertyRegex = new Regex(JsonRegex.Replace("'", "\""));
        private static readonly Regex ClassRegex = new Regex(@"(?<name>'[^,]*?'):(?<value>[{\[].*?[}\]])".Replace("'", "\""));

        private JsonObject()
        {
        }

        // ----- Public methods
        public object Materialize(Type type)
        {
            var instance = Activator.CreateInstance(type);
            foreach (var keyValue in this) {
                var key = keyValue.Key;
                if (key.IsSurroundBy('\"')) {
                    key = key.Substring(1, key.Length - 2);
                }
                SetTo(instance, key, keyValue.Value);
            }
            return instance;
        }

        public void SetTo(object instance, string name, object value)
        {
            var type = instance.GetType();
            var propertyInfo = type.GetProperty(name);
            if (propertyInfo.PropertyType == typeof(string)) {
                var val = (string) value;
                if (val.IsSurroundBy('\"')) {
                    val = val.Substring(1, val.Length - 2);
                }
                propertyInfo.SetValue(instance, val);
            }
            else if (propertyInfo.PropertyType.IsNumeric())
            {
                var val = (string) value;
                if (val.IsSurroundBy('\"')) {
                    val = val.Substring(1, val.Length - 2);
                }

                var parseMethods = propertyInfo
                    .PropertyType
                    .GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .Where(x => x.Name == "Parse")
                    .ToArray();

                var parseMethod = parseMethods.SingleOrDefault(x => x.GetParameters().Count() == 2 && x.GetParameters().Last().ParameterType == typeof(IFormatProvider));
                if (parseMethod != null)
                {
                    var parsedValue = parseMethod.Invoke(null, new object[] { val, CultureInfo.InvariantCulture });
                    propertyInfo.SetValue(instance, parsedValue);
                }
                else
                {
                    parseMethod = parseMethods.SingleOrDefault(x => x.GetParameters().Count() == 1);
                    if (parseMethod == null)
                    {
                        throw new Exception("Unable to parse the value.");
                    }
                    var parsedValue = parseMethod.Invoke(null, new object[] { val });
                    propertyInfo.SetValue(instance, parsedValue);
                }
            }

            else if (propertyInfo.PropertyType.IsClass) {
                var jsonObject = (JsonObject) value;
                var objectProperty = jsonObject.Materialize(propertyInfo.PropertyType);
                propertyInfo.SetValue(instance, objectProperty);
            }
            else
            {
                throw new NotImplementedException();
            }
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

            var classMatches = ClassRegex.Matches(json);
            foreach (Match classMatch in classMatches)
            {
                var name = classMatch.Groups["name"].Value;
                var value = classMatch.Groups["value"].Value;
                jsonObject[name] = Parse(value);
                json = json.Replace(string.Format("{0}:{1}", name, value), "");
            }

            var matches = PropertyRegex.Matches(json);
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
}