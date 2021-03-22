using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer
{
    public class AppShellViewModel
    {
        public ICommand HelpCommand => new Command<string>(url => Launcher.TryOpenAsync(new Uri(url)));
    }
}