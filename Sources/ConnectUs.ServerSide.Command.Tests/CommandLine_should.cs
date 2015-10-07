using NFluent;
using Xunit;

namespace ConnectUs.ServerSide.Command.Tests
{
    public class CommandLine_should
    {
        [Fact]
        public void parse_command_string_with_no_argument_to_command_line()
        {
            const string command = "dir";

            var commandLine = CommandLine.Parse(command);

            Check.That(commandLine.CommandName).IsEqualTo("dir");
            Check.That(commandLine.Arguments).IsEmpty();
        }

        [Fact]
        public void parse_command_string_with_unnamed_argument_to_command_line()
        {
            const string command = "dir c:/";

            var commandLine = CommandLine.Parse(command);

            Check.That(commandLine.CommandName).IsEqualTo("dir");
            Check.That(commandLine.Arguments).ContainsExactly(new CommandArgument("unknown", "c:/"));
        }

        [Fact]
        public void parse_command_string_with_named_argument_to_command_line()
        {
            const string command = "dir --path=mydirectory";

            var commandLine = CommandLine.Parse(command);

            Check.That(commandLine.CommandName).IsEqualTo("dir");
            Check.That(commandLine.Arguments).ContainsExactly(new CommandArgument("path", "mydirectory"));
        }

        [Fact]
        public void parse_command_string_with_no_value_argument_to_command_line()
        {
            const string command = "dir --force";

            var commandLine = CommandLine.Parse(command);

            Check.That(commandLine.CommandName).IsEqualTo("dir");
            Check.That(commandLine.Arguments).ContainsExactly(new CommandArgument("force", null));
        }

        [Fact]
        public void parse_command_string_with_multiple_space_argument_to_command_line()
        {
            const string command = "dir    --force";

            var commandLine = CommandLine.Parse(command);

            Check.That(commandLine.CommandName).IsEqualTo("dir");
            Check.That(commandLine.Arguments).ContainsExactly(new CommandArgument("force", null));
        }

        [Fact]
        public void parse_command_string_with_multiple_arguments_to_command_line()
        {
            const string command = "dir c:/ --force --user=toto --version=1.0.0 --now";

            var commandLine = CommandLine.Parse(command);

            Check.That(commandLine.CommandName).IsEqualTo("dir");
            Check.That(commandLine.Arguments).ContainsExactly(
                new CommandArgument("unknown", "c:/"),
                new CommandArgument("force", null),
                new CommandArgument("user", "toto"),
                new CommandArgument("version", "1.0.0"),
                new CommandArgument("now", null)
            );
        }
    }
}
