using System.Linq;
using System.Reflection;
using SimpleInjector;

namespace ConnectUs.ServerSide.Command
{
    public class CommandLineHandlerLocator : ICommandLineHandlerLocator
    {
        private readonly Container _container;

        public CommandLineHandlerLocator(Container container)
        {
            _container = container;
        }

        public ICommandLineHandler Get(string commandName)
        {
            var type = GetType().Assembly.GetTypes().FirstOrDefault(x =>
            {
                var a = x.GetCustomAttribute<CommandDescriptionAttribute>();
                return a != null && a.CommandName == commandName;
            });

            if (type == null) {
                return null;
            }
            return (ICommandLineHandler) _container.GetInstance(type);
        }
    }
}