using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.SynologyInfo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SynologyInfoPage : BaseContentPage
    {
        public SynologyInfoPage()
        {
            InitializeComponent();
            Title = "Syno";
            var vm = App.Container.Resolve<SynologyInfoViewModel>();
            Start(vm);
        }
    }
}