using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Xam.MonitorMyServer.Views.SelectService;
using Xamarin.Forms;
using Zeroconf;

namespace Doods.Xam.MonitorMyServer.Data
{
    public class ZeroconfHost : DataHost, IQueryShellNavigationObject
    {
        public ZeroconfHost(IReadOnlyList<string> iPAddresses, IReadOnlyDictionary<string, IService> services)
        {
            IPAddresses = iPAddresses;
            Services = services;
            GoToLoginCmd = new Command(GoToLogin);
        }

        public IReadOnlyList<string> IPAddresses { get; }
        public IReadOnlyDictionary<string, IService> Services { get; }

        public ICommand GoToLoginCmd { get; }

        private void GoToLogin()
        {
            //var navigationService = App.Container.Resolve<INavigationService>();
            var navigationService = App.Container.ResolveKeyed<INavigationService>(App.NavigationServiceType);
            // navigationService.NavigateAsync(nameof(LogInPage), this);
            navigationService.NavigateAsync(nameof(SelectSupportedServiciePage),new SynchronizedCacheItemQueryShell(this));
           // var cache = App.Container.Resolve<ISynchronizedCache<object>>();
           //cache.AddOrUpdate(nameof(ZeroconfHost), this);
           //
           // _displayName.Value = dataHost.DisplayName;
           //_hostName.Value = dataHost.IPAddress;
           // if (zeroconfHost.Services.TryGetValue("_ssh._tcp.local.", out var srv)) _port.Value = srv.Port.ToString();
        }
        public string ToQuery()
        {
            return ToQuery(false);

        }

        public string ToQuery(bool forceHttp)
        {
            if (Services.TryGetValue("_ssh._tcp.local.", out var srv))
                return $"DisplayNameQuery={DisplayName}&IPAddressQuery={IPAddress}&PortQuery={srv.Port}";
            
            if(IPAddress.StartsWith("http",true,CultureInfo.CurrentCulture))
            {
                return $"DisplayNameQuery={DisplayName}&IPAddressQuery={Uri.EscapeDataString($"http://{IPAddress}")}";
            }
            ;
            return $"DisplayNameQuery={DisplayName}&IPAddressQuery={Uri.EscapeDataString($"https://{IPAddress}")}";
        }
    }
}