using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Doods.Xam.MonitorMyServer.Views.AptUpdates;
using Doods.Xam.MonitorMyServer.Views.Login;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer
{
    public class AppShellViewModel
    {
      

        public AppShellViewModel()
        {
           // RegisterRoutes();
        }

       

        public ICommand HelpCommand => new Command<string>((url) => Device.OpenUri(new Uri(url)));
    }
}
