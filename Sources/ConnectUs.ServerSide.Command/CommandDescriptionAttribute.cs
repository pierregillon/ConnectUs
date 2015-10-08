using System;

namespace ConnectUs.ServerSide.Command
{
    public class CommandDescriptionAttribute : Attribute
    {
        public string CommandName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
    }
}