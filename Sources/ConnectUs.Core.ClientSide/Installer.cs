namespace ConnectUs.Core.ClientSide
{
    public class Installer : IInstaller
    {
        private const string RootPath = @"C:\Windows\System32\";

        private readonly IEnvironment _environment;

        public Installer(IEnvironment environment)
        {
            _environment = environment;
        }

        public bool IsInstalled
        {
            get { return _environment.CurrentParentFolder.Contains(RootPath); }
        }

        public string Install()
        {
            throw new System.NotImplementedException();
        }
    }
}