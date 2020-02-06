using System;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Repository.Std.Tables;
using Doods.Xam.MonitorMyServer.Views.Login;

namespace Doods.Xam.MonitorMyServer.Data
{
    public class DataHostWrapper : IQueryShellNavigationObject
    {
        private readonly string str;

        public DataHostWrapper(Host host)
        {
            //str = $"DisplayNameQuery={host.HostName}";
            //str = $"PortQuery={host.Port}&DisplayNameQuery={host.HostName}&HostNameQuery={host.Url}";

            var urlStringEscaped = Uri.EscapeDataString(host.Url);
            str =
                $"{nameof(LoginPageViewModel.PortQuery)}={host.Port}&{nameof(LoginPageViewModel.DisplayNameQuery)}={host.HostName}&{nameof(LoginPageViewModel.IPAddressQuery)}={urlStringEscaped}" +
                $"&{nameof(LoginPageViewModel.UserNameQuery)}={host.UserName}&{nameof(LoginPageViewModel.PasswordQuery)}={host.Password}&{nameof(LoginPageViewModel.IsOmvServerQuery)}={host.IsOmvServer}" +
                $"&{nameof(LoginPageViewModel.IsRpiQuery)}={host.IsRpi}&{nameof(LoginPageViewModel.IsSshQuery)}={host.IsSsh}&{nameof(LoginPageViewModel.IdQuery)}={host.Id}" +
                "";
        }

        public string ToQuery()
        {
            return str;
        }
    }
}