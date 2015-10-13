using System;

namespace ConnectUs.ServerSide.Command
{
    public class WindowConsole : IConsole
    {
        private const ConsoleColor InfoColor = ConsoleColor.Gray;
        private const ConsoleColor ErrorColor = ConsoleColor.Red;
        private const ConsoleColor WarningColor = ConsoleColor.DarkYellow;

        public void WriteInfo(string message, params object[] parameters)
        {
            WriteWithColor(string.Format(message, parameters), InfoColor);
        }
        public void WriteError(string message, params object[] parameters)
        {
            WriteWithColor(string.Format(message, parameters), ErrorColor);
        }
        public void WriteWarning(string message, params object[] parameters)
        {
            WriteWithColor(string.Format(message, parameters), WarningColor);
        }

        private static void WriteWithColor(string message, ConsoleColor color)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = previousColor;
        }
    }
}