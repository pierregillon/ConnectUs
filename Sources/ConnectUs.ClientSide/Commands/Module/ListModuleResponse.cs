using System.Collections.Generic;

namespace ConnectUs.ClientSide.Commands.Module
{
    public class ListModuleResponse
    {
        public IEnumerable<ModuleState> Modules { get; set; }
    }
}