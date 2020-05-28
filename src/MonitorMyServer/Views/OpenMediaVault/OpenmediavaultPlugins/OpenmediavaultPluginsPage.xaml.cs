using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Openmediavault.Mobile.Std.Resources;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultPluginsPage : BaseContentPage
    {
        public OpenmediavaultPluginsPage()
        {
            InitializeComponent();
            Title = openmediavault.Plugins;
            var vm = App.Container.Resolve<OpenmediavaultPluginsViewModel>();
            Start(vm);
        }
    }
}