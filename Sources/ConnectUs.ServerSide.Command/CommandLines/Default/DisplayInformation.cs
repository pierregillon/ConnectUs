using ConnectUs.Core.ServerSide.Clients;
using ConnectUs.Core.ServerSide.Decorators;

namespace ConnectUs.ServerSide.Command.CommandLines.Default
{
    [CommandDescription(CommandName = "info", Description = "Display the information of the remote client.")]
    internal class DisplayInformation : CurrentClientCommand
    {
        public DisplayInformation(Context context)
            : base(context)
        {
        }

        protected override void HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var decorator = new ClientInformationDecorator(remoteClient);
            var information = decorator.GetFullClientInformation();

            WriteInfo("** User information");
            WriteInfo("\t- User name : {0}", information.UserName);
            WriteInfo("\t- User domain name : {0}", information.UserDomainName);
            WriteInfo();
            WriteInfo("** Machine information");
            WriteInfo("\t- Machine name : {0}", information.MachineName);
            WriteInfo("\t- Public Ip : {0}", information.PublicIp);
            WriteInfo("\t- Operating system : {0}", information.OperatingSystem);
            WriteInfo();
            WriteInfo("** Network interfaces");
            foreach (var networkInformation in information.NetworkInterfaces) {
                WriteInfo("\t{0}", networkInformation.Name);
                WriteInfo("\t - Description : {0}", networkInformation.Description);
                foreach (var address in networkInformation.Addresses) {
                    WriteInfo("\t - Address : {0}", address);
                }
                WriteInfo();
            }
        }
    }
}