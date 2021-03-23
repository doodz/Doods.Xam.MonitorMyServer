using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.Webmin.States
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebminStatsPage : BaseContentPage
    {
        public WebminStatsPage()
        {
            InitializeComponent();
            Title = "Webmin";
            var vm = App.Container.Resolve<WebminStatsViewModel>();
            Start(vm);
        }
    }
}