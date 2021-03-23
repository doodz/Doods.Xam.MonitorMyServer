using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.Synology.SynoStorage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SynologyStoragePage : BaseContentPage
    {
        public SynologyStoragePage()
        {
            InitializeComponent();
            Title = "Syno";
            var vm = App.Container.Resolve<SynologyStorageViewmodel>();
            Start(vm);
        }
    }
}