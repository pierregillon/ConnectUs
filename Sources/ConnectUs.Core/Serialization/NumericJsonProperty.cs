using System;
using System.Collections.Generic;
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
            var parseMethod = GetParseMethod(type);
            if (parseMethod == null) {
                throw new Exception("Unable to parse the value.");
            }
            if (parseMethod.GetParameters().Length == 2) {
                return parseMethod.Invoke(null, new object[] {_json, CultureInfo.InvariantCulture});
            }
            else {
                return parseMethod.Invoke(null, new object[] {_json});
            }
        }
        public override string ToString()
        {
            return _json;
        }

        private MethodInfo GetParseMethod(Type type)
        {
            var parseMethods = type
                .GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(x => x.Name == "Parse")
                .ToArray();

            return GetParseMethodWithCulture(parseMethods) ?? GetParseMethods(parseMethods);
        }
        private static MethodInfo GetParseMethods(IEnumerable<MethodInfo> parseMethods)
        {
            return parseMethods.SingleOrDefault(x => x.GetParameters().Count() == 1);
        }
        private static MethodInfo GetParseMethodWithCulture(IEnumerable<MethodInfo> parseMethods)
        {
            return parseMethods
                .Where(x =>
                {
                    var parameters = x.GetParameters();
                    return parameters.Count() == 2 &&
                           parameters.Last().ParameterType == typeof (IFormatProvider);
                })
                .SingleOrDefault();
        }
    }
}