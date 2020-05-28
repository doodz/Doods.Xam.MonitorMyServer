using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Xam.MonitorMyServer.Resx;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.About
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : BaseContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            Title = Resource.About;
            var vm = App.Container.Resolve<AboutPageViewModel>();
            Start(vm);
        }
    }
}