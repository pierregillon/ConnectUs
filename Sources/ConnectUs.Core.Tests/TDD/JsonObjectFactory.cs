using System;

namespace ConnectUs.Core.Tests.TDD
{
    internal class JsonObjectFactory
    {
        public static IJsonObject BuildJsonObject(string value)
        {
            if (value.IsSurroundBy('{', '}')) {
                var builder = new JsonClassBuilder();
                return builder.Build(value);
            }
            if (value.IsSurroundBy('\"')) {
                return new StringJsonProperty(value);
            }
            return new NumericJsonProperty(value);
        }

        public static IJsonObject BuildJsonObject(Type type, object instance)
        {
            if (type.IsNumeric()) {
                return new NumericJsonProperty(instance.ToString());
            }
            if (type == typeof (string)) {
                return new StringJsonProperty(instance.ToString().Surround("\""));
            }
            return new JsonClass(instance);
        }
    }
}