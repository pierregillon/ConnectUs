using System;

namespace ConnectUs.Core.Tests.TDD
{
    public class JsonSerializer
    {
        public static object Deserialize(Type type, string json)
        {
            if (string.IsNullOrEmpty(json) || json.Trim() == string.Empty) {
                throw new EmptyJsonException();
            }

            var jsonObject = JsonObject.Parse(json);
            return jsonObject.Materialize(type);
        }
        public static string Serialize(object obj)
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var jsonObject = JsonObject.From(obj);
            return jsonObject.ToString();
        }
    }

    public static class StringExtensions
    {
        public static string Surround(this string value, string element)
        {
            return string.Format("{0}{1}{2}", element, value, element);
        }

        public static string Surround(this string value, string start, string end)
        {
            return string.Format("{0}{1}{2}", start, value, end);
        }

        public static bool IsSurroundBy(this string value, char element)
        {
            if (value.Length < 2) {
                return false;
            }
            return value[0] == element && value[value.Length - 1] == element;
        }
    }

    public static class TypeExtensions
    {
        public static bool IsNumeric(this Type type)
        {
            switch (Type.GetTypeCode(type)) {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                case TypeCode.Object:
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>)) {
                        return Nullable.GetUnderlyingType(type).IsNumeric();
                    }
                    return false;
                default:
                    return false;
            }
        }
    }
}