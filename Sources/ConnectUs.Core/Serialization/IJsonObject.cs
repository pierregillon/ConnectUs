using System;

namespace ConnectUs.Core.Serialization
{
    public interface IJsonObject
    {
        object Materialize(Type type);
    }
}