using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx.Webmin.package_updates;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.NAS.PackageUpdates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PackageUpdatesPage : BaseContentPage
    {
        public PackageUpdatesPage()
        {
            InitializeComponent();
            Title = Webmin_package_updates.view_software;
            var vm = App.Container.Resolve<PackageUpdatesViewModel>();
            Start(vm);
        }
    }
}