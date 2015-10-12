namespace ConnectUs.Modules.Integrated.ModuleManagement
{
    public class LoadModuleRequest
    {
        public string ModuleName { get; set; }

        public LoadModuleRequest()
        {
            
        }
        public LoadModuleRequest(string moduleName)
        {
            ModuleName = moduleName;
        }
    }
}