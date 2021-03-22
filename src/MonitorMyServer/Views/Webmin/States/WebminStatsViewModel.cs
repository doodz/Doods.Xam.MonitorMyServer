using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.Std;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Synology.Webapi.Std;
using Doods.Webmin.Webapi.Std.Datas;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.Webmin.States
{
    public class WebminStatsViewModel : ViewModelWhithState
    {
        private readonly IWebminCgiService _webminService;

        private Stats _stats;


        public WebminStatsViewModel(IWebminCgiService webminService, IConfiguration configuration)
        {
            _webminService = webminService;
            ManageHostsCmd = new Command(ManageHosts);
            ChangeHostCmd = new Command(ChangeHost);
            BannerId = configuration.AdsKey;
        }

        public string BannerId { get; }

        public ICommand ManageHostsCmd { get; }
        public ICommand ChangeHostCmd { get; }

        public Stats Stats
        {
            get => _stats;
            set => SetProperty(ref _stats, value);
        }

        private async void ChangeHost()
        {
            var connctionService = App.Container.Resolve<ConnctionService>();
            await connctionService.ChangeHostTask();
        }

        private void ManageHosts()
        {
            NavigationService.NavigateAsync(nameof(HostManagerPage));
        }

        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                    () => SetLabelsStateItem("Syno", openmediavault.SystemInformation),
                    () =>
                    {
                        UpdateHistory();
                        SetLabelsStateItem("Syno", openmediavault.Done___);
                    });
            }
            catch (Exception e)
            {
                SetLabelsStateItem("Error", e.Message);
            }

            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData()
        {
            return Task.WhenAll(GetStats());
        }

        private async Task GetStats()
        {
            var result = await _webminService.GetStats();
            _stats = result;
        }


        private void UpdateHistory()
        {
            var historyService = App.Container.Resolve<IHistoryService>();
            historyService.CurrentHistoryItem.NombrerPackargeCanBeUpdted = 0;
            historyService.CurrentHistoryItem.LastUpdate = DateTime.Now;
            historyService.SetHistoryAsync(historyService.CurrentHistoryItem.HostId, historyService.CurrentHistoryItem);
        }
    }
}