using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views;

namespace Doods.Xam.MonitorMyServer
{
    public partial class MainPage : BaseContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = Resource.Home;
            var vm = App.Container.Resolve<MainPageViewModel>();
            Start(vm);
        }
    }
}