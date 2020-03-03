using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Openmediavault.Mobile.Std.Resources;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSystemLogs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultSystemLogsPage : BaseContentPage
    {
        public OpenmediavaultSystemLogsPage()
        {
            InitializeComponent();
            Title = openmediavault.SystemLogs;
            var vm = App.Container.Resolve<OpenmediavaultSystemLogsViewModel>();
            Start(vm);
        }
    }
}