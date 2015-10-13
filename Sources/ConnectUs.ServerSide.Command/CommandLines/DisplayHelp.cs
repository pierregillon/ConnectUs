using System;
using System.Linq;
using System.Reflection;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "help", Description = "Display the help.")]
    internal class DisplayHelp : CommandHandler, ICommandLineHandler
    {
        public void Handle(CommandLine commandLine)
        {
            var attributes = GetType()
                .Assembly
                .GetTypes()
                .Where(x => x.GetInterface(typeof (ICommandLineHandler).Name) != null)
                .Select(x => x.GetCustomAttribute<CommandDescriptionAttribute>())
                .Where(x => x != null)
                .OrderBy(x => x.CommandName)
                .ToArray();

            var maxCommandNameLength = attributes.Max(x => x.CommandName.Length);
            var groups = attributes.GroupBy(x => x.Category);

            foreach (var @group in groups) {
                WriteInfo(Environment.NewLine + string.Format("::{0}", @group.Key));
                foreach (var attribute in @group) {
                    WriteInfo(string.Format("\t{0} : {1}", attribute.CommandName.PadRight(maxCommandNameLength), attribute.Description));
                }
            }
        }
    }
}