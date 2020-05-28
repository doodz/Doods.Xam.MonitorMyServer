using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Doods.Framework.Mobile.Std.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems.OpenmediavaultAddFileSystem
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenmediavaultAddFileSystemsPage : BaseContentPage
    {
        public OpenmediavaultAddFileSystemsPage()
        {
            InitializeComponent();
            var vm = App.Container.Resolve<OpenmediavaultAddFileSystemViewModel>();
            Start(vm);
        }
    }
}