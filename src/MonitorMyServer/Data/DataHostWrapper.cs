using System;
using System.Collections.Generic;
using System.Text;
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
    public class DataHostWrapper : IQueryShellNavigationObject
    {

        private string str;
        public DataHostWrapper(Host host)
        {
            //str = $"DisplayNameQuery={host.HostName}";
            //str = $"PortQuery={host.Port}&DisplayNameQuery={host.HostName}&HostNameQuery={host.Url}";


            str = $"PortQuery={host.Port}&DisplayNameQuery={host.HostName}&IPAddressQuery={host.Url}&UserNameQuery={host.UserName}&PasswordQuery={host.Password}&IsOmvServerQuery={host.IsOmvServer}&IsRpiQuery={host.IsRpi}&IsSshQuery={host.IsSsh}&IdQuery={host.Id}";
           
        }

        public string ToQuery()
        {
            return str;
        }
    }
}
