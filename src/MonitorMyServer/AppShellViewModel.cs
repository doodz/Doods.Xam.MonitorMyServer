using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer
{
    public class AppShellViewModel
    {
        public ICommand HelpCommand => new Command<string>(url => Xamarin.Essentials.Launcher.TryOpenAsync(new Uri(url)));
        
    }
}