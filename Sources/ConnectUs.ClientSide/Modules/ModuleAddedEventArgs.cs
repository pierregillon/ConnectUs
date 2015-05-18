using System;

namespace ConnectUs.ClientSide.Modules
{
    public class ModuleAddedEventArgs : EventArgs
    {
        public Module Module { get; set; }
        public ModuleAddedEventArgs(Module module)
        {
            Module = module;
        }
    }
}