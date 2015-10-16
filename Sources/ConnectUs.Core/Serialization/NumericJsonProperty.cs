using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ConnectUs.Core.Serialization
{
    internal class NumericJsonProperty : IJsonObject
    {
        private readonly string _json;

        public NumericJsonProperty(object instance)
        {
            _json = instance.ToString().Trim();
        }
        public NumericJsonProperty(string json)
        {
            _json = json;
        }

        public object Materialize(Type type)
        {
            var parseMethods = type
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.Name == "Parse")
                .ToArray();

            var parseMethod = parseMethods.SingleOrDefault(x => x.GetParameters().Count() == 2 && x.GetParameters().Last().ParameterType == typeof (IFormatProvider));
            if (parseMethod != null) {
                return parseMethod.Invoke(null, new object[] { _json, CultureInfo.InvariantCulture });
            }
            else {
                parseMethod = parseMethods.SingleOrDefault(x => x.GetParameters().Count() == 1);
                if (parseMethod == null) {
                    throw new Exception("Unable to parse the value.");
                }
                return parseMethod.Invoke(null, new object[] { _json });
            }
        }

        public override string ToString()
        {
            return _json;
        }
    }
}