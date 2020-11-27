using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Openmediavault.Mobile.Std.Resources;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.NAS.SharedFolders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SharedFoldersPage : BaseContentPage
    {
        public SharedFoldersPage()
        {
            InitializeComponent();
            Title = openmediavault.SharedFolders;
            var vm = App.Container.Resolve<SharedFoldersViewModel>();
            Start(vm);
        }
    }
}