using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views.About;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.AddCustomCommand
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddCustomCommandPage : BaseContentPage
    {
        public AddCustomCommandPage()
        {
            InitializeComponent();
            Title = Resource.Add_Commands;
            var vm = App.Container.Resolve<AboutPageViewModel>();
            Start(vm);
        }
    }
}