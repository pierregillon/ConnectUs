using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ConnectUs.Core.Serialization
{
    internal class NumericJsonProperty : IJsonObject
    {
        private readonly string _value;

        public NumericJsonProperty(string value)
        {
            _value = value;
        }

        public object Materialize(Type type)
        {
            var val = _value.Replace(" ", "");
            if (val.IsSurroundBy('\"')) {
                val = val.Substring(1, val.Length - 2);
            }

            var parseMethods = type
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.Name == "Parse")
                .ToArray();

            var parseMethod = parseMethods.SingleOrDefault(x => x.GetParameters().Count() == 2 && x.GetParameters().Last().ParameterType == typeof (IFormatProvider));
            if (parseMethod != null) {
                return parseMethod.Invoke(null, new object[] {val, CultureInfo.InvariantCulture});
            }
            else {
                parseMethod = parseMethods.SingleOrDefault(x => x.GetParameters().Count() == 1);
                if (parseMethod == null) {
                    throw new Exception("Unable to parse the value.");
                }
                return parseMethod.Invoke(null, new object[] {val});
            }
        }

        public override string ToString()
        {
            return _value;
        }
    }
}