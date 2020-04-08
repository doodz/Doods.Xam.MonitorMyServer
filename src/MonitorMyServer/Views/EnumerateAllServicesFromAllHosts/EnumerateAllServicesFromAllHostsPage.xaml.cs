using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EnumerateAllServicesFromAllHostsPage : BaseContentPage
    {
        public EnumerateAllServicesFromAllHostsPage()
        {
            InitializeComponent();
            Title = Resource.EnumerateAllServices;
            var vm = App.Container.Resolve<EnumerateAllServicesFromAllHostsViewModel>();
            Start(vm);
        }
    }
}