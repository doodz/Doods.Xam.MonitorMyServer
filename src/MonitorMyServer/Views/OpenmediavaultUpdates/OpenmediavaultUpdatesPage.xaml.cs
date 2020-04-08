using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Openmediavault.Mobile.Std.Resources;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultUpdates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultUpdatesPage : BaseContentPage
    {
        public OpenmediavaultUpdatesPage()
        {
            InitializeComponent();
            Title = openmediavault.UpdateManagement;
            var vm = App.Container.Resolve<OpenmediavaultUpdatesViewModel>();
            Start(vm);
        }
    }
}