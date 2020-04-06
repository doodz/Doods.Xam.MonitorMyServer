using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Repository.Std.Tables;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.Login;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;
using Resource = Doods.Xam.MonitorMyServer.Resx.Resource;

namespace Doods.Xam.MonitorMyServer.Views.HostManager
{
    public class HostManagerPageViewModel : DataTableItemsViewModel<Host>
    {
        private ICommand FindItemCommand;
        public HostManagerPageViewModel()
        {
            Title = Resource.Hosts;
            FindItemCommand = new Command(() => NavigationService.NavigateAsync(nameof(EnumerateAllServicesFromAllHostsPage)));
        }


        protected override void AddItem(object obj)
        {
            NavigationService.NavigateAsync(nameof(LogInPage));
        }

        protected override void EditItem(object obj)
        {
            if (obj == null) return;
            if (obj is Host h) NavigationService.NavigateAsync(nameof(LogInPage), new DataHostWrapper(h));
        }


        protected override void OnFinishLoading(LoadingContext context)
        {
            if (!Items.Any())
                Title = Resource.NoHostsDetected;
        }

        public override IEnumerable<CommandItem> GetToolBarItemDescriptions()
        {

            var lst =base.GetToolBarItemDescriptions().ToList();
            var image1 = SvgIconTarget.CheckCircle.ResourceFile;
            var image3 = new FileImageSource();
            image3.File = image1;


          

          var cmd = new CommandItem(CommandId.AnalyseThematique)
            {
                Text = "Find",
                IsPrimary = true,
                Command = FindItemCommand,
                Icon = image3
            };
            lst.Add(cmd);
            return lst;
        }
    }
}