using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConnectUs.Core.Tests.TDD
{
    public class JsonClass : Dictionary<string, IJsonObject>, IJsonObject
    {
        public JsonClass(){}
        public JsonClass(object obj)
        {
            foreach (var property in GetPropertiesToSerialize(obj)) {
                this[property.Name.Surround("\"")] = JsonObjectFactory.BuildJsonObject(property.PropertyType, property.GetValue(obj));
            }
        }

        // ----- Public methods
        public object Materialize(Type type)
        {
            var instance = Activator.CreateInstance(type);
            foreach (var keyValue in this) {
                var propertyInfo = Key(type, keyValue.Key);
                if (propertyInfo != null) {
                    propertyInfo.SetValue(instance, keyValue.Value.Materialize(propertyInfo.PropertyType));
                }
            }
            return instance;
        }

        // ----- Overrides
        public override string ToString()
        {
            var elements = this.Select(x => x.Key + ":" + x.Value).ToArray();
            return string.Join(",", elements).Surround("{", "}");
        }

        // ----- Utils
        private static PropertyInfo Key(Type type, string propertyName)
        {
            if (propertyName.IsSurroundBy('\"')) {
                propertyName = propertyName.Substring(1, propertyName.Length - 2);
            }
            return type.GetProperty(propertyName);
        }
    
        private static IEnumerable<PropertyInfo> GetPropertiesToSerialize(object origin)
        {
            return origin
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty);
        }
    }
}