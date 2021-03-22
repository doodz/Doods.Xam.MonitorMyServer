using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.Std;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Synology.Webapi.Std.Datas;
using Doods.Synology.Webapi.Std.NewFolder;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.SynologyInfo
{
    public class SynologyInfoViewModel : ViewModelWhithState
    {
        private readonly ISynologyCgiService _synoService;
        private NetworkInfo _networkInfo;
        private SynologyServiceInfo _servicesInfo;
        private StorageInfo _storageInfo;
        private SystemInfo _systemInfo;

        private SynologyUpgradeStatus _upgradeStatus;

        public SynologyInfoViewModel(ISynologyCgiService synoService, IConfiguration configuration)
        {
            _synoService = synoService;
            ManageHostsCmd = new Command(ManageHosts);
            ChangeHostCmd = new Command(ChangeHost);
            BannerId = configuration.AdsKey;
        }

        public string BannerId { get; }

        public ICommand ManageHostsCmd { get; }
        public ICommand ChangeHostCmd { get; }

        public SynologyServiceInfo ServicesInfo
        {
            get => _servicesInfo;
            set => SetProperty(ref _servicesInfo, value);
        }

        public StorageInfo StorageInfo
        {
            get => _storageInfo;
            set => SetProperty(ref _storageInfo, value);
        }

        public NetworkInfo NetworkInfo
        {
            get => _networkInfo;
            set => SetProperty(ref _networkInfo, value);
        }

        public SynologyUpgradeStatus UpgradeStatus
        {
            get => _upgradeStatus;
            set => SetProperty(ref _upgradeStatus, value);
        }

        public SystemInfo SystemInfo
        {
            get => _systemInfo;
            set => SetProperty(ref _systemInfo, value);
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
            return Task.WhenAll(GetSystemInfo(), GetUpgradeStatus(), GetNetworkInfo(), GetStorageInfo(),
                GetServicesInfo());
        }

        private async Task GetSystemInfo()
        {
            var result = await _synoService.GetSystemInfo();
            SystemInfo = result;
        }

        private async Task GetUpgradeStatus()
        {
            var result = await _synoService.GetUpgradeStatus();
            UpgradeStatus = result;
        }

        private async Task GetNetworkInfo()
        {
            var result = await _synoService.GetNetworkInfo();
            NetworkInfo = result;
        }

        private async Task GetStorageInfo()
        {
            var result = await _synoService.GetStorageInfo();
            StorageInfo = result;
        }

        private void UpdateHistory()
        {
            var historyService = App.Container.Resolve<IHistoryService>();
            historyService.CurrentHistoryItem.NombrerPackargeCanBeUpdted = 0;
            historyService.CurrentHistoryItem.LastUpdate = DateTime.Now;
            historyService.SetHistoryAsync(historyService.CurrentHistoryItem.HostId, historyService.CurrentHistoryItem);
        }

        private async Task GetServicesInfo()
        {
            var result = await _synoService.GetServicesInfo();
            ServicesInfo = result;
        }
    }
}