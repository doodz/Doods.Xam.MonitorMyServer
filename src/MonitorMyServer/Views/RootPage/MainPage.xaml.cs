using System;
using Autofac;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views;
using Rg.Plugins.Popup.Services;

namespace Doods.Xam.MonitorMyServer
{
    public partial class MainPage : BaseContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = Resource.Home;
            var vm = App.Container.Resolve<MainPageViewModel>();
            Start(vm);
        }

        private async void OnOpenListViewPage(object sender, EventArgs e)
        {
            var page = new PopupListViewPage();

            await PopupNavigation.Instance.PushAsync(page);
        }
    }
}