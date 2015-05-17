namespace ConnectUs.ServerSide.Application.Controls
{
    public class CommandLine
    {
        public string Command { get; set; }
        public string Result { get; set; }

        public CommandLine(string command)
        {
            Command = command;
        }
    }
}