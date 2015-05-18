using System;

namespace ConnectUs.ClientSide.Modules
{
    public class ModuleRemovedEventArgs : EventArgs
    {
        public Module Module { get; private set; }
        public ModuleRemovedEventArgs(Module module)
        {
            Module = module;
        }
    }
}