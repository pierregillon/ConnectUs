using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConnectUs.ServerSide.Command
{
    public class CommandLine
    {
        public string CommandName { get; private set; }
        public IEnumerable<CommandArgument> Arguments { get; private set; }

        public CommandLine(string commandName, IEnumerable<CommandArgument> arguments)
        {
            CommandName = commandName;
            Arguments = arguments;
        }

        public static CommandLine Parse(string commandLine)
        {
            var values = commandLine.Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
            var commandName = values[0];
            var arguments = new List<CommandArgument>();
            foreach (var argument in values.Skip(1)) {
                CommandArgument commandArgument;
                if (TryParseParameterizedArgument(argument, out commandArgument)) {
                    arguments.Add(commandArgument);
                }
                else if (TryParseNoValueArgument(argument, out commandArgument)) {
                    arguments.Add(commandArgument);
                }
                else {
                    arguments.Add(new CommandArgument("unknown", argument));
                }
            }
            return new CommandLine(commandName, arguments);
        }

        // ----- Internal logics
        private static bool TryParseNoValueArgument(string argument, out CommandArgument commandArgument)
        {
            var match = Regex.Match(argument, @"--(?<argumentName>[^\ ]*)=(?<argumentValue>[^\ ]*)");
            if (match.Success) {
                commandArgument = new CommandArgument(match.Groups["argumentName"].Value, match.Groups["argumentValue"].Value);
            }
            else {
                commandArgument = null;
            }
            return commandArgument != null;
        }

        private static bool TryParseParameterizedArgument(string argument, out CommandArgument commandArgument)
        {
            var match = Regex.Match(argument, @"--(?<argumentName>[^\ \=]*)[\ \n]?$");
            if (match.Success) {
                commandArgument = new CommandArgument(match.Groups["argumentName"].Value, null);
            }
            else {
                commandArgument = null;
            }
            return commandArgument != null;
        }
    }
}