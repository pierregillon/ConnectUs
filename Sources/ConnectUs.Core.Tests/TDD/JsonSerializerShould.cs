using System;
using System.Collections.Generic;
using System.Globalization;
using ConnectUs.Core.Serialization;
using NFluent;
using Xunit;

namespace ConnectUs.Core.Tests.TDD
{
    public class JsonSerializerShould
    {
        private readonly JsonSerializer _jsonSerializer;

        private readonly CarBuilder _aCar = new CarBuilder();
        private readonly MotorBuilder _aMotor = new MotorBuilder();
        private readonly ParkingBuilder _aParking = new ParkingBuilder();

        public JsonSerializerShould()
        {
            _jsonSerializer = new JsonSerializer();
        }

        [Theory]
        [InlineData("     ")]
        [InlineData("")]
        [InlineData(null)]
        public void throw_error_when_trying_to_deserialize_empty_json(string json)
        {
            Action code = () => _jsonSerializer.Deserialize(typeof (object), json);

            Check.ThatCode(code).Throws<EmptyJsonException>();
        }

        [Theory]
        [InlineData(typeof (object), "{}")]
        [InlineData(typeof (Exception), "{}")]
        public void deserialize_json_with_no_data_should_return_empty_object(Type type, string json)
        {
            var result = _jsonSerializer.Deserialize(type, json);

            Check.That(result).Not.IsNull();
            Check.That(result.GetType()).IsEqualTo(type);
        }

        [Theory]
        [InlineData("{\"MyValue\":100}", 100)]
        [InlineData("{\"MyValue\":-100}", -100)]
        public void deserialize_json_with_long(string json, long expectedValue)
        {
            var result = _jsonSerializer.Deserialize<MyObject<long>>(json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":120}", 120)]
        [InlineData("{\"MyValue\":-33}", -33)]
        public void deserialize_json_with_short(string json, short expectedValue)
        {
            var result = _jsonSerializer.Deserialize<MyObject<short>>(json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":15}", 15)]
        [InlineData("{\"MyValue\":-15}", -15)]
        public void deserialize_json_with_integer(string json, int expectedValue)
        {
            var result = _jsonSerializer.Deserialize<MyObject<int>>(json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":120.25}", 120.25d)]
        [InlineData("{\"MyValue\":-20.25}", -20.25d)]
        public void deserialize_json_with_double(string json, double expectedValue)
        {
            var result = _jsonSerializer.Deserialize<MyObject<double>>(json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":120.25}", 120.25)]
        [InlineData("{\"MyValue\":-20.25}", -20.25)]
        public void deserialize_json_with_decimal(string json, decimal expectedValue)
        {
            var result = _jsonSerializer.Deserialize<MyObject<decimal>>(json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":120.25}", 120.25f)]
        [InlineData("{\"MyValue\":-20.25}", -20.25f)]
        public void deserialize_json_with_float(string json, float expectedValue)
        {
            var result = _jsonSerializer.Deserialize<MyObject<float>>(json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"MyValue\":\"hello world\"}", "hello world")]
        [InlineData("{\"MyValue\":\"\"}", "")]
        public void deserialize_json_with_string(string json, string expectedValue)
        {
            var result = _jsonSerializer.Deserialize<MyObject<string>>(json);

            Check.That(result).Not.IsNull();
            Check.That(result.MyValue).IsEqualTo(expectedValue);
        }

        [Theory]
        [InlineData("{\"Name\":\"myname\",\"Value\":\"myvalue\",\"Ticks\":1000}")]
        public void deserialize_json_with_simple_dataset(string json)
        {
            var result = _jsonSerializer.Deserialize<MySimpleObject>(json);

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
            var result = _jsonSerializer.Deserialize<MySimpleObject>(json);

            Check.That(result).Not.IsNull();
            Check.That(result.Name).IsEqualTo("myname");
            Check.That(result.Value).IsEqualTo("myvalue");
            Check.That(result.Ticks).IsEqualTo(1000);
        }

        [Theory]
        [InlineData("{\"Motor\":{\"Brand\":\"Yamaha\",\"Couple\":1250},\"Name\":\"Test\"}")]
        public void deserialize_json_with_sub_object(string json)
        {
            var car = _jsonSerializer.Deserialize<Car>(json);

            Check.That(car).Not.IsNull();
            Check.That(car.Name).IsEqualTo("Test");
            Check.That(car.Motor).Not.IsNull();
            Check.That(car.Motor.Brand).IsEqualTo("Yamaha");
            Check.That(car.Motor.Couple).IsEqualTo(1250);
        }

        [Theory]
        [InlineData("[]", new int[0])]
        [InlineData("[1,2,3]", new[] { 1, 2, 3 })]
        [InlineData("[1,  2,    -3]", new[] { 1, 2, -3 })]
        public void deserialize_int_json_array(string json, int[] values)
        {
            var integers = _jsonSerializer.Deserialize<List<int>>(json);

            Check.That(integers).Not.IsNull();
            Check.That(integers).ContainsExactly(values);
        }

        [Theory]
        [InlineData("[]", new string[0])]
        [InlineData("[\"test\",\"hello\",\"world\"]", new[] { "test", "hello", "world" })]
        [InlineData("[\"test\"  ,  \"hel  lo\"   ,   \"world\"]", new[] { "test", "hel  lo", "world" })]
        public void deserialize_string_json_array(string json, string[] values)
        {
            var elements = _jsonSerializer.Deserialize<List<string>>(json);

            Check.That(elements).Not.IsNull();
            Check.That(elements).ContainsExactly(elements);
        }

        [Fact]
        public void throw_error_when_trying_to_serialize_null_object()
        {
            Action code = () => _jsonSerializer.Serialize(null);

            Check.ThatCode(code).Throws<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(120)]
        [InlineData(-33)]
        public void serialize_object_with_long_property(long value)
        {
            var json = _jsonSerializer.Serialize(new MyObject<long>(value));

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("{'MyValue':{0}}".Replace("{0}", value.ToString()).Replace("'", "\""));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(120)]
        [InlineData(-33)]
        public void serialize_object_with_int_property(int value)
        {
            var json = _jsonSerializer.Serialize(new MyObject<int>(value));

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("{'MyValue':{0}}".Replace("{0}", value.ToString()).Replace("'", "\""));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(0.3652f)]
        [InlineData(-66.256f)]
        public void serialize_object_with_float_property(float value)
        {
            var json = _jsonSerializer.Serialize(new MyObject<float>(value));

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("{'MyValue':{0}}".Replace("{0}", value.ToString(CultureInfo.InvariantCulture)).Replace("'", "\""));
        }

        [Fact]
        public void serialize_simple_object()
        {
            var obj = new MySimpleObject
            {
                Name = "James BOND",
                Value = "test23",
                Ticks = 1236
            };
            var json = _jsonSerializer.Serialize(obj);

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("{'Name':'James BOND','Value':'test23','Ticks':1236}".Replace("'", "\""));
        }

        [Fact]
        public void serialize_simple_object_with_sub_object()
        {
            var obj = new Car
            {
                Motor = new Motor
                {
                    Brand = "Yamaha",
                    Couple = 1235
                },
                Name = "Test"
            };
            var json = _jsonSerializer.Serialize(obj);

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("{'Motor':{'Brand':'Yamaha','Couple':1235},'Name':'Test'}".Replace("'", "\""));
        }

        [Fact]
        public void serialize_collection_of_int()
        {
            var numbers = new[]{1, 2, 3, 5, 6, 9, -5};
            var json = _jsonSerializer.Serialize(numbers);

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("[1,2,3,5,6,9,-5]");
        }

        [Fact]
        public void serialize_collection_of_object()
        {
            var johnCar = _aCar
                .WithName("Test")
                .WithMotor(
                    _aMotor
                        .WithBrand("Yamaha")
                        .WithCouple(1235)
                        .Build()
                )
                .Build();

            var robCar = _aCar
                .WithName("Test2")
                .WithMotor(
                    _aMotor
                        .WithBrand("Audi")
                        .WithCouple(900)
                        .Build()
                )
                .Build();

            var json = _jsonSerializer.Serialize(new[] {johnCar, robCar});

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("[" +
                                       "{'Motor':{'Brand':'Yamaha','Couple':1235},'Name':'Test'},".Replace("'", "\"") +
                                       "{'Motor':{'Brand':'Audi','Couple':900},'Name':'Test2'}".Replace("'", "\"") +
                                       "]");
        }

        [Fact]
        public void serialize_object_with_array()
        {
            var parking = _aParking
                .With(_aCar
                    .WithName("Test")
                    .WithMotor(
                        _aMotor
                            .WithBrand("Yamaha")
                            .WithCouple(1235)
                            .Build()
                    ).Build())
                
                .With(
                    _aCar
                        .WithName("Test2")
                        .WithMotor(
                            _aMotor
                                .WithBrand("Audi")
                                .WithCouple(900)
                                .Build()
                        )
                        .Build())
                .Build();

            var json = _jsonSerializer.Serialize(parking);

            Check.That(json).Not.IsNull();
            Check.That(json).IsEqualTo("{" +
                                       "'Cars':".Replace("'", "\"") +
                                       "[" +
                                       "{'Motor':{'Brand':'Yamaha','Couple':1235},'Name':'Test'},".Replace("'", "\"") +
                                       "{'Motor':{'Brand':'Audi','Couple':900},'Name':'Test2'}".Replace("'", "\"") +
                                       "]".Replace("'", "\"") +
                                       "}");
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

            // ReSharper disable once UnusedMember.Local
            public MyObject() {}
            public MyObject(T value)
            {
                MyValue = value;
            }
        }

        private class Car
        {
            public Motor Motor { get; set; }
            public string Name { get; set; }
        }

        private class CarBuilder
        {
            private string _name;
            private Motor _motor;

            public CarBuilder WithName(string name)
            {
                _name = name;
                return this;
            }
            public CarBuilder WithMotor(Motor motor)
            {
                _motor = motor;
                return this;
            }
            public Car Build()
            {
                return new Car
                {
                    Name = _name,
                    Motor = _motor
                };
            }
        }

        private class Motor
        {
            public string Brand { get; set; }
            public long Couple { get; set; }
        }

        private class MotorBuilder
        {
            private string _brand;
            private int _couple;
            public MotorBuilder WithBrand(string brand)
            {
                _brand = brand;
                return this;
            }
            public MotorBuilder WithCouple(int couple)
            {
                _couple = couple;
                return this;
            }
            public Motor Build()
            {
                return new Motor
                {
                    Brand = _brand,
                    Couple = _couple
                };
            }
        }

        private class Parking
        {
            public IEnumerable<Car> Cars { get; set; }
        }

        private class ParkingBuilder
        {
            private readonly List<Car> _cars = new List<Car>();

            public ParkingBuilder With(Car car)
            {
                _cars.Add(car);
                return this;
            }
            public Parking Build()
            {
                return new Parking
                {
                    Cars = _cars
                };
            }
        }
    }

}