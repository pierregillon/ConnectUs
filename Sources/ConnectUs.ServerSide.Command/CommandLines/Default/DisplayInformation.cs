using System;
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

        protected override string HandleInternal(CommandLine commandLine, IRemoteClient remoteClient)
        {
            var decorator = new ClientInformationDecorator(remoteClient);
            var information = decorator.GetFullClientInformation();

            Console.WriteLine("** User information");
            Console.WriteLine("\t- User name : {0}", information.UserName);
            Console.WriteLine("\t- User domain name : {0}", information.UserDomainName);
            Console.WriteLine();
            Console.WriteLine("** Machine information");
            Console.WriteLine("\t- Machine name : {0}", information.MachineName);
            Console.WriteLine("\t- Public Ip : {0}", information.PublicIp);
            Console.WriteLine("\t- Operating system : {0}", information.OperatingSystem);
            Console.WriteLine();
            Console.WriteLine("** Network interfaces");
            foreach (var networkInformation in information.NetworkInterfaces) {
                Console.WriteLine("\t{0}", networkInformation.Name);
                Console.WriteLine("\t - Description : {0}", networkInformation.Description);
                foreach (var address in networkInformation.Addresses) {
                    Console.WriteLine("\t - Address : {0}", address);
                }
                Console.WriteLine();
            }
            return string.Empty;
        }
    }
}