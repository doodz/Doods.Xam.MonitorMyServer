using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;
using System;

namespace Doods.Xam.MonitorMyServer.Data
{
    public class CustomCommandSshWrapper : IQueryShellNavigationObject
    {
        private string str;
        public CustomCommandSshWrapper(CustomCommandSsh command)
        {
           var commandStringEscaped = Uri.EscapeDataString(command.CommandString);
            str = $"IdQuery={command.Id}&NameQuery={command.Name}&CommandStringQuery={commandStringEscaped}";

        }

        public string ToQuery()
        {
            return str;
        }
    }
}