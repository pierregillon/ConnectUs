using System;

namespace ConnectUs.ClientSide.ModuleManagement
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