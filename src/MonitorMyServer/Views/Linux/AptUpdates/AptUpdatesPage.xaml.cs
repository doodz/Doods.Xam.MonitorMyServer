using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.AptUpdates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AptUpdatesPage : BaseContentPage
    {
        public AptUpdatesPage()
        {
            InitializeComponent();
            Title = Resource.Updates;
            var vm = App.Container.Resolve<AptUpdatesPageViewModel>();
            Start(vm);
        }
    }
}