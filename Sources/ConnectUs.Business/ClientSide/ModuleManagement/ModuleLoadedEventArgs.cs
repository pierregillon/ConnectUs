using System;

namespace ConnectUs.ClientSide.ModuleManagement
{
    public class ModuleLoadedEventArgs : EventArgs
    {
        public Module Module { get; set; }
        public ModuleLoadedEventArgs(Module module)
        {
            Module = module;
        }
    }
}