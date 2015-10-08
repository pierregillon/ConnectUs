namespace ConnectUs.ClientSide.Commands.Module
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