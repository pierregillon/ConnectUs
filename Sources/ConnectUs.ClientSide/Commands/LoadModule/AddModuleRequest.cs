namespace ConnectUs.ClientSide.Commands.LoadModule
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