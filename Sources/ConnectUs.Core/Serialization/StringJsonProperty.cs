using System;

namespace ConnectUs.Core.Serialization
{
    internal class StringJsonProperty : IJsonObject
    {
        private readonly string _json;

        public StringJsonProperty(object instance)
        {
            _json = ((string) instance).Surround("\"");
        }
        public StringJsonProperty(string json)
        {
            _json = json;
        }

        public object Materialize(Type type)
        {
            var val = _json;
            if (val.IsSurroundBy('\"')) {
                val = val.Substring(1, val.Length - 2);
            }
            return val;
        }

        public override string ToString()
        {
            return _json;
        }
    }
}