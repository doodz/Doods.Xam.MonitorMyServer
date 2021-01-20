using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.Linux.DisksUsage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DisksUsagePage : BaseContentPage
    {
        public DisksUsagePage()
        {
            InitializeComponent();
            Title = "Bash";
            var vm = App.Container.Resolve<DisksUsageViewmodel>();
            Start(vm);
        }
    }
}