using System;

namespace ConnectUs.Core.ModuleManagement
{
    public class ModuleException : Exception
    {
        public ModuleException(string message) : base(message) { }
        public ModuleException(string message, Exception innerException) : base(message, innerException) { }
    }
}