using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Repository.Std.Tables;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Renci.SshNet.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.Login
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : BaseContentPage
    {

      
        public LogInPage(ZeroconfHost zeroconfHost)
        {
            InitializeComponent();
            Title = Resource.Home;
            var vm = App.Container.Resolve<LoginPageViewModel>();
            vm.SetHost(zeroconfHost);
            Start(vm);
        }

        public LogInPage()
        {
            InitializeComponent();
            Title = Resource.Home;
            var vm = App.Container.Resolve<LoginPageViewModel>();
            Start(vm);
        }

        public LogInPage(Host host)
        {
            InitializeComponent();
            Title = Resource.Home;
            var vm = App.Container.Resolve<LoginPageViewModel>();
            vm.SetHost(host);
            Start(vm);
        }
    }
}