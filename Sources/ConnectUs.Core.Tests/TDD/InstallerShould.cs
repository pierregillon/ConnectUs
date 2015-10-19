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

        public InstallerShould()
        {
            _environment = new Mock<IEnvironment>();
            _installer = new Installer(_environment.Object);
        }

        [Theory]
        [InlineData(@"c:\")]
        [InlineData("D:/")]
        [InlineData(@"C:\Users\Alfred\Desktop")]
        public void detect_application_as_not_installed_when_located_in_(string applicationParentFolder)
        {
            _environment.Setup(x => x.CurrentParentFolder).Returns(applicationParentFolder);

            Check.That(_installer.IsInstalled).IsFalse();
        }

        [Theory]
        [InlineData(@"C:\Windows\System32\")]
        [InlineData(@"C:\Windows\System32\MyFolder\")]
        public void detect_application_as_installed_when_located_in_(string applicationParentFolder)
        {
            _environment.Setup(x => x.CurrentParentFolder).Returns(applicationParentFolder);

            Check.That(_installer.IsInstalled).IsTrue();
        }
    }
}
