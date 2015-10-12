using System;

namespace ConnectUs.Core.ModuleManagement
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