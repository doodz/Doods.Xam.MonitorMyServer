using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Doods.Xam.MonitorMyServer.Views.Login;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer
{
    public class AppShellViewModel
    {
        private readonly Dictionary<string, Type> _routes = new Dictionary<string, Type>();
        public Dictionary<string, Type> Routes => _routes;

        public AppShellViewModel()
        {
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            _routes.Add("Login", typeof(LogInPage));
            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }

        public ICommand HelpCommand => new Command<string>((url) => Device.OpenUri(new Uri(url)));
    }
}
