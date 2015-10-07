using System.Collections.Generic;

namespace ConnectUs.ClientSide.Commands.LoadModule
{
    public class ListModuleResponse
    {
        public IEnumerable<ModuleState> Modules { get; set; }
    }
}