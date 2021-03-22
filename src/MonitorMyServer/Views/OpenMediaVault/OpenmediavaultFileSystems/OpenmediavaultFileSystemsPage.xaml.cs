using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultFileSystemsPage : BaseContentPage
    {
        public OpenmediavaultFileSystemsPage()
        {
            InitializeComponent();
            var vm = App.Container.Resolve<OpenmediavaultFileSystemsViewModel>();
            Start(vm);
        }
    }
}