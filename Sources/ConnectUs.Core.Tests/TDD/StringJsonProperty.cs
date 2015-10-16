using System;

namespace ConnectUs.Core.Tests.TDD
{
    internal class StringJsonProperty : IMaterializable
    {
        private readonly string _value;

        public StringJsonProperty(string value)
        {
            _value = value;
        }

        public object Materialize(Type type)
        {
            var val = _value;
            if (val.IsSurroundBy('\"')) {
                val = val.Substring(1, val.Length - 2);
            }
            return val;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}