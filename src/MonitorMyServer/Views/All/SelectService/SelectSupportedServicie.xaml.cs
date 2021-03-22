using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Openmediavault.Mobile.Std.Resources;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.SelectService
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectSupportedServiciePage : BaseContentPage
    {
        public SelectSupportedServiciePage()
        {
            InitializeComponent();
            Title = openmediavault.Services;

            var vm = App.Container.Resolve<SelectSupportedServicieViewModel>();
            Start(vm);
        }
    }
}