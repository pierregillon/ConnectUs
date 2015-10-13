using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using NFluent;
using Xunit;

namespace ConnectUs.Core.Tests.TDD
{
    public class JsonSerializerShould
    {
        [Theory]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData(null)]
        public void throw_error_when_trying_to_parse_empty_json(string json)
        {
            Action code = () => JsonSerializer.Deserialize(typeof (object), json);

            Check.ThatCode(code).Throws<EmptyJsonException>();
        }

        [Theory]
        [InlineData(typeof (object), "{}")]
        [InlineData(typeof (Exception), "{}")]
        public void parse_json_with_no_data_should_return_empty_object(Type type, string json)
        {
            var result = JsonSerializer.Deserialize(type, json);

            Check.That(result).Not.IsNull();
            Check.That(result.GetType()).IsEqualTo(type);
        }

        [Theory]
        [InlineData("{\"MyValue\":100}", 100)]
        [InlineData("{\"MyValue\":-100}", -100)]
        public void parse_json_with_long(string json, long expectedValue)
        {
            var result = (MyObject<long>) JsonSerializer.Deserialize(typeof (MyObject<long>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":120}", 120)]
        [InlineData("{\"MyValue\":-33}", -33)]
        public void parse_json_with_short(string json, short expectedValue)
        {
            var result = (MyObject<short>) JsonSerializer.Deserialize(typeof (MyObject<short>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":15}", 15)]
        [InlineData("{\"MyValue\":-15}", -15)]
        public void parse_json_with_integer(string json, int expectedValue)
        {
            var result = (MyObject<int>) JsonSerializer.Deserialize(typeof (MyObject<int>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":\"hello world\"}", "hello world")]
        [InlineData("{\"MyValue\":\"\"}", "")]
        public void parse_json_with_string(string json, string expectedValue)
        {
            var result = (MyObject<string>)JsonSerializer.Deserialize(typeof(MyObject<string>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"Name\":\"myname\",\"Value\":\"myvalue\",\"Ticks\":1000}")]
        public void parse_json_with_simple_dataset(string json)
        {
            var result = (MySimpleObject)JsonSerializer.Deserialize(typeof(MySimpleObject), json);

            Check.That(result).Not.IsNull();
            Check.That(result.Name).IsEqualTo("myname");
            Check.That(result.Value).IsEqualTo("myvalue");
            Check.That(result.Ticks).IsEqualTo(1000);
        }

        [Theory]
        [InlineData("{\"Name\"  :  \"myname\"  ,  \"Value\" :   \"myvalue\" ,  \"Ticks\" : 1000}")]
        [InlineData("{  \"Name\"  :\"myname\",  \"Value\"     :   \"myvalue\" ,  \"Ticks\" : 1000     }")]
        public void parse_json_with_spaces(string json)
        {
            var result = (MySimpleObject)JsonSerializer.Deserialize(typeof(MySimpleObject), json);

            Check.That(result).Not.IsNull();
            Check.That(result.Name).IsEqualTo("myname");
            Check.That(result.Value).IsEqualTo("myvalue");
            Check.That(result.Ticks).IsEqualTo(1000);
        }

        private class MySimpleObject
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int Ticks { get; set; }
        }

        private class MyObject<T>
        {
            public T MyValue { get; set; }
        }
    }

    public class EmptyJsonException : Exception
    {
    }

    public class JsonSerializer
    {
        public static object Deserialize(Type type, string json)
        {
            if (string.IsNullOrEmpty(json) || json.Trim() == string.Empty) {
                throw new EmptyJsonException();
            }
            var instance = Activator.CreateInstance(type);
            var properties = GetProperties(json);
            foreach (var property in properties) {
                property.AffectTo(instance);
            }
            return instance;
        }

        private static IEnumerable<JsonProperty> GetProperties(string json)
        {
            var regex = new Regex(@"'(?<name>[^:]*)'\ *:\ *'?(?<value>[^,^}^']*)'?".Replace("'", "\""));
            var matches = regex.Matches(json);
            foreach (Match match in matches) {
                yield return new JsonProperty(match.Groups["name"].Value, match.Groups["value"].Value);
            }
        }
    }

    internal class JsonProperty
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public JsonProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public void AffectTo(object instance)
        {
            var type = instance.GetType();
            var propertyInfo = type.GetProperty(Name);
            if (propertyInfo.PropertyType == typeof (string)) {
                propertyInfo.SetValue(instance, Value);
            }
            else {
                var parseMethod = propertyInfo.PropertyType.GetMethods(BindingFlags.Static | BindingFlags.Public).SingleOrDefault(x => x.Name == "Parse" && x.GetParameters().Count() == 1);
                if (parseMethod == null) {
                    throw new Exception("Unable to parse the value.");
                }
                var parsedValue = parseMethod.Invoke(null, new object[] {Value});
                propertyInfo.SetValue(instance, parsedValue);
            }
        }
    }
}