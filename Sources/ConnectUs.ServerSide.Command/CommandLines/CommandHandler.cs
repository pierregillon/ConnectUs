using System;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    public abstract class CommandHandler
    {
        private readonly IConsole _console = new WindowConsole();

        protected void WriteInfo(string message = null, params object[] parameters)
        {
            _console.WriteInfo(message ?? Environment.NewLine, parameters);
        }
        protected void WriteError(string message = null, params object[] parameters)
        {
            _console.WriteError(message ?? Environment.NewLine, parameters);
        }
        protected void WriteWarning(string message = null, params object[] parameters)
        {
            _console.WriteWarning(message ?? Environment.NewLine, parameters);
        }
    }
}