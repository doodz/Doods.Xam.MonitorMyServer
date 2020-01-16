using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultStatistics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.Settings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : BaseContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            Title = "Settings";
            var vm = App.Container.Resolve<SettingsPAgeViewModel>();
            Start(vm);
        }
    }
}