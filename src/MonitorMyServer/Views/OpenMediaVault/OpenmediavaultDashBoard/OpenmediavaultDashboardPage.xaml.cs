using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultDashboardPage : BaseContentPage
    {
        public OpenmediavaultDashboardPage()
        {
            InitializeComponent();
            Title = "OMV";
            var vm = App.Container.Resolve<OpenmediavaultDashboardViewModel>();
            Start(vm);
        }
    }
}