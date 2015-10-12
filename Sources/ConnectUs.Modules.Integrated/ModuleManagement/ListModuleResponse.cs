using System.Collections.Generic;

namespace ConnectUs.Modules.Integrated.ModuleManagement
{
    public class ListModuleResponse
    {
        public IEnumerable<ModuleState> Modules { get; set; }
    }
}