using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.ApiClientBase.Std.Classes;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Lists;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts;
using Doods.Xam.MonitorMyServer.Views.Login;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.HostManager
{
    public class HostManagerPageViewModel : DataTableItemsViewModel<Host>
    {
        private readonly ICommand FindItemCommand;

        public HostManagerPageViewModel()
        {
            Title = Resource.Hosts;
            FindItemCommand = new Command(() =>
                NavigationService.NavigateAsync(nameof(EnumerateAllServicesFromAllHostsPage)));
            SelectItemCommand = new Command(SelectItem);
        }

        public ObservableRangeCollection<HostViewModel> ItemsView { get; } =
            new ObservableRangeCollection<HostViewModel>();

        public ICommand SelectItemCommand { get; }


        protected override void AddItem(object obj)
        {
            var t = NavigationService.NavigateAsync(nameof(LogInPage));
            if (t.IsFaulted)
            {
                var msg = t.Exception?.Message;
            }
        }


        private void SelectItem(object obj)
        {
            var connctionService = App.Container.Resolve<ConnctionService>();
            if (obj is HostViewModel h)
                connctionService.SelectHost(h.Host).ConfigureAwait(false);
        }

        protected override void EditItem(object obj)
        {
            if (obj == null) return;
            if (obj is HostViewModel h)
            {
                var t = NavigationService.NavigateAsync(nameof(LogInPage), new DataHostWrapper(h.Host));
                if (t.IsFaulted)
                {
                    var msg = t.Exception?.Message;
                }
            }
        }

        protected override void DeleteItem(object obj)
        {
            if (obj == null) return;
            if (obj is HostViewModel h)
                base.DeleteItem(h.Host);
        }

        protected override void OnFinishLoading(LoadingContext context)
        {
            if (!Items.Any())
                Title = Resource.NoHostsDetected;
        }

        public override IEnumerable<CommandItem> GetToolBarItemDescriptions()
        {
            var lst = base.GetToolBarItemDescriptions().ToList();
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

        protected override async Task RefreshData()
        {
            await base.RefreshData();

            ItemsView.ReplaceRange(Items.Select(h => new HostViewModel(h)));

            foreach (var hostViewModel in ItemsView) await hostViewModel.GetMacString();
        }
    }

    public enum HostStatus
    {
        Loading,
        Online,
        Unreachable,
        HostnameInvalid,
        NoHostname
    }

    public static class PingServiceExtensions
    {
        public static HostStatus ToHostStatus(this PingResult result)
        {
            if (result == PingResult.Success) return HostStatus.Online;
            if (result == PingResult.Unreachable) return HostStatus.Unreachable;
            if (result == PingResult.HostNotFound) return HostStatus.HostnameInvalid;
            return HostStatus.Unreachable;
        }
    }

    //public static class HostExtensions
    //{
    //    public static string GetMacString(this Host value)
    //    {
    //        var adrBytes = value?.MacAddress ?? throw new ArgumentNullException(nameof(value));
    //        return string.Join(":", from z in adrBytes select z.ToString("X2", CultureInfo.InvariantCulture));
    //    }
    //}
}