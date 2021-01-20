using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Openmediavault.Mobile.Std.Resources;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.Linux.Logs
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogsPage : BaseContentPage
    {
        public LogsPage()
        {
            InitializeComponent();
            Title = openmediavault.SystemLogs;
            var vm = App.Container.Resolve<LogsViewmodel>();
            Start(vm);
        }
    }
}