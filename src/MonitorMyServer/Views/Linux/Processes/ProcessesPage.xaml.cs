using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.Processes2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProcessesPage : BaseContentPage
    {
        public ProcessesPage()
        {
            InitializeComponent();
            Title = Resource.Processes;
            var vm = App.Container.Resolve<ProcessesPageViewModel>();
            Start(vm);
        }
    }
}