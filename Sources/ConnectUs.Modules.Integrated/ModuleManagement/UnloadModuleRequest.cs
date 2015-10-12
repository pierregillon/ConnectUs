namespace ConnectUs.Modules.Integrated.ModuleManagement
{
    public class UnloadModuleRequest
    {
        public string ModuleName { get; set; }

        public UnloadModuleRequest()
        {
            
        }
        public UnloadModuleRequest(string moduleName)
        {
            ModuleName = moduleName;
        }
    }
}