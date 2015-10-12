namespace ConnectUs.Modules.Integrated.ModuleManagement
{
    public class AddModuleRequest
    {
        public string ModuleName { get; set; }

        public AddModuleRequest()
        {
            
        }
        public AddModuleRequest(string moduleName)
        {
            ModuleName = moduleName;
        }
    }
}