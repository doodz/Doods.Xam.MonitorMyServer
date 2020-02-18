using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.Tests
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : BaseContentPage
    {
        public TestPage()
        {
            InitializeComponent();
            Title = Resource.Home;
            var vm = App.Container.Resolve<TestPageViewModel>();
            Start(vm);
        }
    }
}