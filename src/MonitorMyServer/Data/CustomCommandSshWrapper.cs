using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;

namespace Doods.Xam.MonitorMyServer.Data
{
    public class CustomCommandSshWrapper : IQueryShellNavigationObject
    {
        private string str;
        public CustomCommandSshWrapper(CustomCommandSsh command)
        {
          
            str = $"IdQuery={command.Id}&NameQuery={command.Name}&CommandStringQuery={command.CommandString}";

        }

        public string ToQuery()
        {
            return str;
        }
    }
}