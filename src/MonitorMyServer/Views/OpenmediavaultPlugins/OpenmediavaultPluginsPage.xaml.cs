using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultPluginsPage : BaseContentPage
    {
        public OpenmediavaultPluginsPage()
        {
            InitializeComponent();
            Title = "OMV plugins";
            var vm = App.Container.Resolve<OpenmediavaultPluginsViewModel>();
            Start(vm);
        }
    }
}