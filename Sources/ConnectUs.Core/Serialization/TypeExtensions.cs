using System;
using System.Linq;

namespace ConnectUs.Core.Serialization
{
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

        public static bool Implements(this Type classType, Type interfaceType)
        {
            var genericArguments = classType.GetGenericArguments();
            if (genericArguments.Any()) {
                return classType.GetInterface(interfaceType.MakeGenericType(genericArguments).Name) != null;
            }
            else {
                return classType.GetInterface(interfaceType.Name) != null;
            }
        }
    }
}