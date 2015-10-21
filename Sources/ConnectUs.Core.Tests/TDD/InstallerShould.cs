using ConnectUs.Core.ClientSide;
using Moq;
using NFluent;
using Xunit;

namespace ConnectUs.Core.Tests.TDD
{
    public class InstallerShould
    {
        private readonly Mock<IEnvironment> _environment;
        private readonly Installer _installer;
        private readonly Mock<IRegistry> _registry;
        private readonly Mock<IFileService> _fileService;

        public InstallerShould()
        {
            _environment = new Mock<IEnvironment>();
            _fileService = new Mock<IFileService>();
            _registry = new Mock<IRegistry>();
            _installer = new Installer(_environment.Object, _fileService.Object, _registry.Object);
        }

        [Theory]
        [InlineData(@"c:\application.exe")]
        [InlineData("D:/application.exe")]
        [InlineData(@"C:\Users\Alfred\Desktop\application.exe")]
        public void detect_application_as_not_installed_when_located_in_(string applicationPath)
        {
            _environment.Setup(x => x.ApplicationPath).Returns(applicationPath);

            Check.That(_installer.IsInstalled).IsFalse();
        }

        [Theory]
        [InlineData(@"C:\Windows\System32\application.exe")]
        [InlineData(@"C:\Windows\System32\MyFolder\application.exe")]
        public void detect_application_as_installed_when_located_in_(string applicationPath)
        {
            _environment.Setup(x => x.ApplicationPath).Returns(applicationPath);

            Check.That(_installer.IsInstalled).IsTrue();
        }

        [Fact]
        public void copy_executable_to_safe_place_when_installing()
        {
            const string originPath = @"C:\test.exe";
            const string targetPath = @"C:\Windows\System32\test.exe";
            _environment.Setup(x => x.ApplicationPath).Returns(originPath);

            _installer.Install();

            _fileService.Verify(x => x.Copy(originPath, targetPath), Times.Once);
        }

        [Fact]
        public void add_target_file_path_in_startup_registry_when_installing()
        {
            _environment.Setup(x => x.ApplicationPath).Returns(@"C:\test.exe");

            _installer.Install();

            _registry.Verify(x => x.AddFileToStartupRegistry(@"C:\Windows\System32\test.exe"), Times.Once);
        }
    }
}
