using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;
using System;

namespace Doods.Xam.MonitorMyServer.Data
{
    public class CustomCommandSshWrapper : IQueryShellNavigationObject
    {
        private readonly string _str;
        public CustomCommandSshWrapper(CustomCommandSsh command)
        {
           var commandStringEscaped = Uri.EscapeDataString(command.CommandString);
            _str = $"IdQuery={command.Id}&NameQuery={command.Name}&CommandStringQuery={commandStringEscaped}";

        }

        public string ToQuery()
        {
            return _str;
        }
    }
}