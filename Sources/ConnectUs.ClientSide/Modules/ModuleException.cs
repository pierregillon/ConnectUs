using System;

namespace ConnectUs.ClientSide.Modules
{
    public class ModuleException : Exception
    {
        public ModuleException(string message) : base(message) {}
    }
}