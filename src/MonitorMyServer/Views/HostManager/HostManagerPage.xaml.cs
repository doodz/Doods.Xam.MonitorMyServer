using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.HostManager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HostManagerPage : BaseContentPage
    {
        public HostManagerPage()
        {
            InitializeComponent();
            Title = Resource.Home;
            var vm = App.Container.Resolve<HostManagerPageViewModel>();
            Start(vm);
        }
    }
}