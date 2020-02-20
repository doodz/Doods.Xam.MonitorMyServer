using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Std;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.std.Data.V5;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard
{
    public class OpenmediavaultDashboardViewModel : ViewModelWhithState
    {
        private readonly IOmvService _sshService;
        private OMVInformations _OMVInformations;
        private string _text = string.Empty;

        public OpenmediavaultDashboardViewModel(IOmvService sshService, IConfiguration configuration)
        {
            _sshService = sshService;
            UpdatesCmd = new Command(Updates);
            CheckCmd = new Command(Check);
            ManageHostsCmd = new Command(ManageHosts);
            ChangeHostCmd = new Command(ChangeHost);
            BannerId = configuration.AdsKey;
        }

        //public ObservableRangeCollection<SystemInformation> SystemInformation { get; } =
        //    new ObservableRangeCollection<SystemInformation>();
        public string BannerId { get; }

        public ICommand ManageHostsCmd { get; }
        public ICommand ChangeHostCmd { get; }

        public ICommand UpdatesCmd { get; }
        public ICommand CheckCmd { get; }

        public OMVInformations OMVInformations
        {
            get => _OMVInformations;
            set => SetProperty(ref _OMVInformations, value);
        }


        public ObservableRangeCollection<OmvFilesystems> Filesystems { get; } =
            new ObservableRangeCollection<OmvFilesystems>();

        public ObservableRangeCollection<Devices> Devices { get; } = new ObservableRangeCollection<Devices>();
        public ObservableRangeCollection<Upgraded> Upgradeds { get; } = new ObservableRangeCollection<Upgraded>();

        public ObservableRangeCollection<ServicesStatus> ServicesStatus { get; } =
            new ObservableRangeCollection<ServicesStatus>();

        private async void ChangeHost()
        {
            var connctionService = App.Container.Resolve<ConnctionService>();
            await connctionService.ChangeHostTask();
        }

        private void ManageHosts()
        {
            NavigationService.NavigateAsync(nameof(HostManagerPage));
        }

        private async void Check(object obj)
        {
            await ViewModelStateItem.RunActionAsync(async () =>
                {
                    var filename = await _sshService.UpdateAptList();

                    await Task.Delay(TimeSpan.FromSeconds(3));
                    await CheckRunningAsync(filename);
                }, () => SetLabelsStateItem(openmediavault.Updates, openmediavault.CheckingForNewUpdates___),
                () => SetLabelsStateItem(openmediavault.Updates, openmediavault.Done___));
        }


        private async Task<bool> CheckRunningAsync(string filename)
        {
            if (!await _sshService.IsRunning(filename)) return false;

            await Task.Delay(TimeSpan.FromSeconds(3));
            return await CheckRunningAsync(filename);
        }

        private async Task<bool> GetOutputAsync(string filename, int pos)
        {
            var result = await _sshService.GetOutput<string>(filename, pos);
            if (result.Running)
            {
                _text += result.Content;
                await Task.Delay(TimeSpan.FromSeconds(3));
                return await GetOutputAsync(filename, result.Pos);
            }

            return false;
        }

        private async void Updates(object obj)
        {
            _text = string.Empty;
            await ViewModelStateItem.RunActionAsync(async () =>
                {
                    var filename = await _sshService.UpgradeAptList(Upgradeds.Select(u => u.Name));

                    await Task.Delay(TimeSpan.FromSeconds(3));
                    await GetOutputAsync(filename, 0);
                    await GetUpgraded();
                }, () => SetLabelsStateItem(openmediavault.Updates, $"Upgrading {Upgradeds.Count} pakages"),
                () => { SetLabelsStateItem(openmediavault.Updates, openmediavault.Done___); });
        }


        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                    () => SetLabelsStateItem("OMV", openmediavault.SystemInformation),
                    () => { SetLabelsStateItem("OMV", openmediavault.Done___); });
            }
            catch (Exception e)
            {
                SetLabelsStateItem("Error", e.Message);
            }

            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData()
        {
            return Task.WhenAll(GetSystemInformation(), GetServicesStatus(), GetUpgraded(), GetFilesystems(),
                GetDevices());
        }

        private async Task GetUpgraded()
        {
            var result = await _sshService.GetUpgraded();
            Upgradeds.ReplaceRange(result);
        }

        private async Task GetServicesStatus()
        {
            var result = await _sshService.GetServicesStatus();
            ServicesStatus.ReplaceRange(result);
        }

        private async Task GetFilesystems()
        {
            var result = await _sshService.GetFilesystems();
            Filesystems.ReplaceRange(result);
        }

        private async Task GetDevices()
        {
            var result = await _sshService.GetDevices();
            Devices.ReplaceRange(result);
        }


        private async Task GetSystemInformation()
        {
            var result = await _sshService.GetSystemInformations();
            OMVInformations = result;
            //SystemInformation.ReplaceRange(result);
        }

        private void GotoSetting(object o)
        {
            NavigationService.NavigateAsync(nameof(OpenmediavaultSettingsPage));
        }


        public override IEnumerable<CommandItem> GetToolBarItemDescriptions()
        {
            var image1 = SvgIconTarget.AddBox.ResourceFile;
            //var image2 = new SvgCachedImage
            //{
            //    Source = SvgIconTarget.AddBox.ResourceFile,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    HeightRequest = 100,
            //    DownsampleToViewSize = true,
            //    Aspect = Aspect.AspectFill,
            //    TransformPlaceholders = false,
            //    LoadingPlaceholder = "loading.png",
            //    ErrorPlaceholder = "error.png"
            //};


            var image3 = new FileImageSource {File = image1};

            yield return new CommandItem(CommandId.AnalyseThematique)
            {
                Text = openmediavault.Settings,
                IsPrimary = true,
                Command = new Command(GotoSetting),
                Icon = image3
            };
        }
    }
}