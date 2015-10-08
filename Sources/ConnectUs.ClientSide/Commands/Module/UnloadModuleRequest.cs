namespace ConnectUs.ClientSide.Commands.Module
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