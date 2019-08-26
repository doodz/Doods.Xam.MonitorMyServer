using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultSettingsPage : BaseContentPage
    {
        public OpenmediavaultSettingsPage()
        {
            InitializeComponent();
            Title = "OMV settings";
            var vm = App.Container.Resolve<OpenmediavaultSettingsViewModel>();
            Start(vm);
        }
    }
}