using System;

namespace ConnectUs.Core.ModuleManagement
{
    public class ModuleUnloadedEventArgs : EventArgs
    {
        public Module Module { get; private set; }
        public ModuleUnloadedEventArgs(Module module)
        {
            Module = module;
        }
    }
}