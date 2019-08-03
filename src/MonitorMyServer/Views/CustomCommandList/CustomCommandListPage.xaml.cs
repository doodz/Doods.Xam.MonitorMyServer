using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views.About;
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
            var vm = App.Container.Resolve<AboutPageViewModel>();
            Start(vm);
        }
    }
}