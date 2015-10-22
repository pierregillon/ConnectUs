using ConnectUs.Core.ClientSide;
using Moq;
using NFluent;
using Xunit;

namespace ConnectUs.Core.Tests.TDD
{
    public class InstallerShould
    {
        private const string SOME_FILE_PATH = @"C:\Windows\System32\test.exe";
        private const string SOME_REGISTRY_PATH = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Setup";
        private const string SOME_REGISTRY_VALUE = "FireWall";
        private const string SOME_FILE = @"c:\test.exe";

        private readonly Mock<IEnvironment> _environment;
        private readonly Installer _installer;
        private readonly Mock<IRegistry> _registry;
        private readonly Mock<IFileService> _fileService;

        public InstallerShould()
        {
            _environment = new Mock<IEnvironment>();

            _fileService = new Mock<IFileService>();
            _fileService.Setup(x => x.GenerateRandomFileName()).Returns("xxx.exe");

            _registry = new Mock<IRegistry>();
            _installer = new Installer(_environment.Object, _fileService.Object, _registry.Object);
        }

        [Fact]
        public void detect_application_as_installed_when_file_path_in_registry_and_file_exists()
        {
            const string filePath = @"c:\test.exe";

            _registry.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(filePath);
            _registry.Setup(x => x.IsRegisteredAtStartup(It.IsAny<string>())).Returns(true);
            _fileService.Setup(x => x.Exists(filePath)).Returns(true);

            Check.That(_installer.IsInstalled).IsTrue();
        }

        [Fact]
        public void detect_application_as_partially_installed_when_file_is_missing()
        {
            _registry.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(SOME_FILE);
            _registry.Setup(x => x.IsRegisteredAtStartup(It.IsAny<string>())).Returns(true);
            _fileService.Setup(x => x.Exists(SOME_FILE)).Returns(false);

            Check.That(_installer.IsPartiallyInstalled).IsTrue();
        }

        [Fact]
        public void detect_application_as_partially_installed_when_file_not_registered_at_startup()
        {
            _registry.Setup(x => x.Get(It.IsAny<string>(), It.IsAny<string>())).Returns(SOME_FILE);
            _registry.Setup(x => x.IsRegisteredAtStartup(It.IsAny<string>())).Returns(false);
            _fileService.Setup(x => x.Exists(SOME_FILE)).Returns(true);

            Check.That(_installer.IsPartiallyInstalled).IsTrue();
        }

        [Fact]
        public void copy_new_executable_to_safe_place_when_installing()
        {
            _environment.Setup(x => x.ApplicationPath).Returns(@"C:\test.exe");

            _installer.Install();

            _fileService.Verify(x => x.Copy(@"C:\test.exe", It.IsRegex(@"C:\\Windows\\System32\\.+\.exe")), Times.Once);
        }

        [Fact]
        public void generate_a_new_name_for_executable_when_installing()
        {
            _fileService.Setup(x => x.GenerateRandomFileName()).Returns("myName.exe");

            _installer.Install();

            _fileService.Verify(x => x.Copy(It.IsAny<string>(), @"C:\Windows\System32\myName.exe"), Times.Once);
        }

        [Fact]
        public void add_target_file_path_in_startup_registry_when_installing()
        {
            _fileService.Setup(x => x.GenerateRandomFileName()).Returns("myName.exe");

            _installer.Install();

            _registry.Verify(x => x.AddFileToStartupRegistry(@"C:\Windows\System32\myName.exe"), Times.Once);
        }

        [Fact]
        public void add_target_file_path_in_specific_registry_when_installing()
        {
            _fileService.Setup(x => x.GenerateRandomFileName()).Returns("myName.exe");

            _installer.Install();

            _registry.Verify(x => x.Add(SOME_REGISTRY_PATH, SOME_REGISTRY_VALUE, @"C:\Windows\System32\myName.exe"), Times.Once);
        }

        [Fact]
        public void remove_target_file_path_of_startup_registry_when_uninstalling()
        {
            _registry.Setup(x => x.Get(SOME_REGISTRY_PATH, SOME_REGISTRY_VALUE)).Returns(SOME_FILE_PATH);

            _installer.Uninstall();

            _registry.Verify(x => x.RemoveFileFromStartupRegistry(SOME_FILE_PATH), Times.Once);
        }

        [Fact]
        public void remove_target_file_path_of_specific_registry_when_uninstalling()
        {
            _registry.Setup(x => x.Get(SOME_REGISTRY_PATH, SOME_REGISTRY_VALUE)).Returns(SOME_FILE_PATH);

            _installer.Uninstall();

            _registry.Verify(x => x.Remove(SOME_REGISTRY_PATH, SOME_REGISTRY_VALUE), Times.Once);
        }
    }
}
