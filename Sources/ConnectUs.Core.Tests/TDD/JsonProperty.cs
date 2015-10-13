using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ConnectUs.Core.Tests.TDD
{
    internal class JsonProperty
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public JsonProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public void SetTo(object instance)
        {
            var type = instance.GetType();
            var propertyInfo = type.GetProperty(Name);
            if (propertyInfo.PropertyType == typeof (string)) {
                propertyInfo.SetValue(instance, Value);
            }
            else {
                var parseMethods = propertyInfo
                    .PropertyType
                    .GetMethods(BindingFlags.Static | BindingFlags.Public)
                    .Where(x=>x.Name == "Parse")
                    .ToArray();

                var parseMethod = parseMethods.SingleOrDefault(x => x.GetParameters().Count() == 2 && x.GetParameters().Last().ParameterType == typeof (IFormatProvider));
                if (parseMethod != null) {
                    var parsedValue = parseMethod.Invoke(null, new object[] {Value, CultureInfo.InvariantCulture});
                    propertyInfo.SetValue(instance, parsedValue);
                }
                else {
                    parseMethod = parseMethods.SingleOrDefault(x => x.GetParameters().Count() == 1);
                    if (parseMethod == null) {
                        throw new Exception("Unable to parse the value.");
                    }
                    var parsedValue = parseMethod.Invoke(null, new object[] { Value });
                    propertyInfo.SetValue(instance, parsedValue);
                }
            }
        }
    }
}