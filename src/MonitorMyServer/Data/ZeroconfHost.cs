using System.Collections.Generic;
using System.Windows.Input;
using Autofac;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Xam.MonitorMyServer.Views.Login;
using Xamarin.Forms;
using Zeroconf;

namespace Doods.Xam.MonitorMyServer.Data
{
    public class ZeroconfHost : DataHost
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
            var navigationService = App.Container.Resolve<INavigationService>();
            navigationService.NavigateAsync(nameof(LogInPage), this);
        }
    }
}