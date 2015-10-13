using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        [InlineData(typeof(object), "{}")]
        [InlineData(typeof(Exception), "{}")]
        public void parse_json_with_no_data_should_return_empty_object(Type type, string json)
        {
            var result = JsonSerializer.Deserialize(type, json);

            Check.That(result).Not.IsNull();
            Check.That(result.GetType()).IsEqualTo(type);
        }

        [Theory]
        [InlineData("{\"Name\":\"myname\",\"Value\":\"myvalue\",\"Ticks\":1000}")]
        public void parse_json_with_simple_data_should_return_object(string json)
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
        public void parse_json_with_spaces_should_return_object(string json)
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
                property.Set(instance);
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

        public void Set(object instance)
        {
            var type = instance.GetType();
            var propertyInfo = type.GetProperty(Name);
            if (propertyInfo.PropertyType == typeof (string)) {
                propertyInfo.SetValue(instance, Value);
            }
            else {
                propertyInfo.SetValue(instance, int.Parse(Value));
            }
        }
    }
}
