using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
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