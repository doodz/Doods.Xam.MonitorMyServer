using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using AutoMapper;
using Doods.Framework.Mobile.Std.Models;
using Doods.Framework.Mobile.Std.Mvvm;
using Doods.Framework.Std;
using Doods.Framework.Std.Lists;
using Doods.Xam.MonitorMyServer.Data;
using Doods.Xam.MonitorMyServer.Resx;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;
using Zeroconf;

namespace Doods.Xam.MonitorMyServer.Views.EnumerateAllServicesFromAllHosts
{
    public class EnumerateAllServicesFromAllHostsViewModel : ViewModel
    {
        private ViewModelStateItem _viewModelStateItem;

        public ViewModelStateItem ViewModelStateItem
        {
            get => _viewModelStateItem;
            private set => SetProperty(ref _viewModelStateItem, value);
        }

        public ICommand EnumerateCommand => new Command(async () => await EnumerateAllServicesFromAllHosts());


        public ObservableRangeCollection<ZeroconfHost> ZeroconfHosts { get; } =
            new ObservableRangeCollection<ZeroconfHost>();

        public async Task EnumerateAllServicesFromAllHosts()
        {
            ZeroconfHosts.Clear();
            //var domains = await ZeroconfResolver.BrowseDomainsAsync(TimeSpan.FromSeconds(5));
            //var responses1 = await ZeroconfResolver.ResolveAsync(domains.Select(g => g.Key), TimeSpan.FromSeconds(5));
            var responses = await ZeroconfResolver.ResolveAsync(new[]
                {"_ssh._tcp.local.", "_https._tcp.local.", "_http._tcp.local."});
            var mapper = App.Container.Resolve<IMapper>();

            var toto = mapper.Map<IReadOnlyList<IZeroconfHost>, List<ZeroconfHost>>(responses);
            //var toto = mapper.Map<IEnumerable<IZeroconfHost>, IEnumerable<ZeroconfHost>>(responses);
            ZeroconfHosts.AddRange(toto);
        }

        protected override void OnInitializeLoading(LoadingContext context)
        {
            ViewModelStateItem = new ViewModelStateItem(this);
            ViewModelStateItem.Title = Resource.SearchingHosts;
            ViewModelStateItem.Description = Resource.ClickForSearching;
            ViewModelStateItem.IsRunning = true;
            ViewModelStateItem.Color = Color.Blue;
            //ViewModelStateItem = viewModelStateItem;
        }

        protected override async Task InternalLoadAsync(LoadingContext context)
        {
            await EnumerateAllServicesFromAllHosts();
        }

        protected override void OnFinishLoading(LoadingContext context)
        {
            //var viewModelStateItem = new ViewModelStateItem(this);
            //ViewModelStateItem.Title = "";
            //ViewModelStateItem.Description = "";
            ViewModelStateItem.IsRunning = false;
            ViewModelStateItem.Color = Color.Green;
            // ViewModelStateItem = viewModelStateItem;
        }
    }
}