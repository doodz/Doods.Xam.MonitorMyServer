using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.Login
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : BaseContentPage
    {
        public LogInPage()
        {
            InitializeComponent();
            Title = Resource.Home;
            var vm = App.Container.Resolve<LoginPageViewModel>();
            Start(vm);
        }
    }
}