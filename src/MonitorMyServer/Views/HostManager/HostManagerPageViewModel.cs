using System.Linq;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Repository.Std.Tables;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.Login;

namespace Doods.Xam.MonitorMyServer.Views.HostManager
{
    public class HostManagerPageViewModel : DataTableItemsViewModel<Host>
    {
        public HostManagerPageViewModel()
        {
            Title = Resource.Hosts;
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
    }
}