using System;

namespace ConnectUs.Core.Tests.TDD
{
    public interface IJsonObject
    {
        object Materialize(Type type);
    }
}