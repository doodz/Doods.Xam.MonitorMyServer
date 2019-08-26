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

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultStatistics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultStatisticsPage : BaseContentPage
    {
        public OpenmediavaultStatisticsPage()
        {
            InitializeComponent();
            Title = "OMV";
            var vm = App.Container.Resolve<OpenmediavaultStatisticsViewModel>();
            Start(vm);
        }
    }
}