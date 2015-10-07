﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ConnectUs.ServerSide.Command.CommandLines
{
    [CommandDescription(CommandName = "help", Description = "Display the help.")]
    internal class DisplayHelp : ICommandLineHandler
    {
        public string Handle(CommandLine commandLine)
        {
            var attributes = GetType()
                .Assembly
                .GetTypes()
                .Select(x => x.GetCustomAttribute<CommandDescriptionAttribute>())
                .Where(x=>x!=null)
                .OrderBy(x=>x.CommandName)
                .ToArray();

            var elements = new List<string>();
            foreach (var attribute in attributes) {
                elements.Add(string.Format(" - {0} : {1}", attribute.CommandName.PadRight(10), attribute.Description));
            }
            return "Available commands : " + Environment.NewLine + string.Join(Environment.NewLine, elements.ToArray());
        }
    }
}