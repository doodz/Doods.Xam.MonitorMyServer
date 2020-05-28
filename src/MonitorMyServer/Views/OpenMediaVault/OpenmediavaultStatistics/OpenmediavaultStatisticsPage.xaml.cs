using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Openmediavault.Mobile.Std.Resources;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultStatistics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultStatisticsPage : BaseContentPage
    {
        public OpenmediavaultStatisticsPage()
        {
            InitializeComponent();
            Title = openmediavault.PerformanceStatistics;
            var vm = App.Container.Resolve<OpenmediavaultStatisticsViewModel>();
            Start(vm);
        }
    }
}