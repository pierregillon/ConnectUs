using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void throw_error_when_trying_to_deserialize_empty_json(string json)
        {
            Action code = () => JsonSerializer.Deserialize(typeof (object), json);

            Check.ThatCode(code).Throws<EmptyJsonException>();
        }

        [Theory]
        [InlineData(typeof (object), "{}")]
        [InlineData(typeof (Exception), "{}")]
        public void deserialize_json_with_no_data_should_return_empty_object(Type type, string json)
        {
            var result = JsonSerializer.Deserialize(type, json);

            Check.That(result).Not.IsNull();
            Check.That(result.GetType()).IsEqualTo(type);
        }

        [Theory]
        [InlineData("{\"MyValue\":100}", 100)]
        [InlineData("{\"MyValue\":-100}", -100)]
        public void deserialize_json_with_long(string json, long expectedValue)
        {
            var result = (MyObject<long>) JsonSerializer.Deserialize(typeof (MyObject<long>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":120}", 120)]
        [InlineData("{\"MyValue\":-33}", -33)]
        public void deserialize_json_with_short(string json, short expectedValue)
        {
            var result = (MyObject<short>) JsonSerializer.Deserialize(typeof (MyObject<short>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":15}", 15)]
        [InlineData("{\"MyValue\":-15}", -15)]
        public void deserialize_json_with_integer(string json, int expectedValue)
        {
            var result = (MyObject<int>) JsonSerializer.Deserialize(typeof (MyObject<int>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":120.25}", 120.25d)]
        [InlineData("{\"MyValue\":-20.25}", -20.25d)]
        public void deserialize_json_with_double(string json, double expectedValue)
        {
            var result = (MyObject<double>)JsonSerializer.Deserialize(typeof(MyObject<double>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":120.25}", 120.25)]
        [InlineData("{\"MyValue\":-20.25}", -20.25)]
        public void deserialize_json_with_decimal(string json, decimal expectedValue)
        {
            var result = (MyObject<decimal>)JsonSerializer.Deserialize(typeof(MyObject<decimal>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":120.25}", 120.25f)]
        [InlineData("{\"MyValue\":-20.25}", -20.25f)]
        public void deserialize_json_with_float(string json, float expectedValue)
        {
            var result = (MyObject<float>)JsonSerializer.Deserialize(typeof(MyObject<float>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":\"hello world\"}", "hello world")]
        [InlineData("{\"MyValue\":\"\"}", "")]
        public void deserialize_json_with_string(string json, string expectedValue)
        {
            var result = (MyObject<string>) JsonSerializer.Deserialize(typeof (MyObject<string>), json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"Name\":\"myname\",\"Value\":\"myvalue\",\"Ticks\":1000}")]
        public void deserialize_json_with_simple_dataset(string json)
        {
            var result = (MySimpleObject) JsonSerializer.Deserialize(typeof (MySimpleObject), json);

            Check.That(result).Not.IsNull();
            Check.That(result.Name).IsEqualTo("myname");
            Check.That(result.Value).IsEqualTo("myvalue");
            Check.That(result.Ticks).IsEqualTo(1000);
        }

        [Theory]
        [InlineData("{\"Name\"  :  \"myname\"  ,  \"Value\" :   \"myvalue\" ,  \"Ticks\" : 1000}")]
        [InlineData("{  \"Name\"  :\"myname\",  \"Value\"     :   \"myvalue\" ,  \"Ticks\" : 1000     }")]
        public void deserialize_json_with_spaces(string json)
        {
            var result = (MySimpleObject) JsonSerializer.Deserialize(typeof (MySimpleObject), json);

            Check.That(result).Not.IsNull();
            Check.That(result.Name).IsEqualTo("myname");
            Check.That(result.Value).IsEqualTo("myvalue");
            Check.That(result.Ticks).IsEqualTo(1000);
        }

        [Theory(Skip = "temp")]
        [InlineData("{\"Motor\":{\"Brand\":\"Yamaha\",\"Couple\":1250}}")]
        public void deserialize_json_with_sub_object(string json)
        {
            var car = (Car)JsonSerializer.Deserialize(typeof(Car), json);

            Check.That(car).Not.IsNull();
            Check.That(car.Motor).Not.IsNull();
            Check.That(car.Motor.Brand).IsEqualTo("Yamaha");
            Check.That(car.Motor.Couple).IsEqualTo(1250);
        }

        [Fact]
        public void throw_error_when_trying_to_serialize_null_object()
        {
            Action code = () => JsonSerializer.Serialize(null);

            Check.That(code).Throws<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(120)]
        [InlineData(-33)]
        public void serialize_json_with_long(long value)
        {
            var json = JsonSerializer.Serialize(new MyObject<long>(value));

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("{'MyValue':{0}}".Replace("{0}", value.ToString()).Replace("'", "\""));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(120)]
        [InlineData(-33)]
        public void serialize_json_with_long(int value)
        {
            var json = JsonSerializer.Serialize(new MyObject<int>(value));

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("{'MyValue':{0}}".Replace("{0}", value.ToString()).Replace("'", "\""));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.3652f)]
        [InlineData(-66.256f)]
        public void serialize_json_with_float(float value)
        {
            var json = JsonSerializer.Serialize(new MyObject<float>(value));

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("{'MyValue':{0}}".Replace("{0}", value.ToString()).Replace("'", "\""));
        }

        // ----- Internal classes

        private class MySimpleObject
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public int Ticks { get; set; }
        }

        private class MyObject<T>
        {
            public T MyValue { get; set; }

            public MyObject(){}
            public MyObject(T value)
            {
                MyValue = value;
            }
        }

        private class Car
        {
            public Motor Motor { get; set; }
        }

        private class Motor
        {
            public string Brand { get; set; }
            public long Couple { get; set; }
        }
    }
}