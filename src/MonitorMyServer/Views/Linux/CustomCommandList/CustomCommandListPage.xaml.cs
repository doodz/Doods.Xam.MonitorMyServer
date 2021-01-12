using System;
using System.ComponentModel;
using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.CustomCommandList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomCommandListPage : BaseContentPage
    {
        public CustomCommandListPage()
        {
            InitializeComponent();
            Title = Resource.Commands;
            var vm = App.Container.Resolve<CustomCommandListPageViewModel>();
            Start(vm);
        }

        private void ScrollView_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ScrollView.ContentSize))
                MyScrollView.ScrollToAsync(this.CmdBox, ScrollToPosition.End, true);
        }
    }
}