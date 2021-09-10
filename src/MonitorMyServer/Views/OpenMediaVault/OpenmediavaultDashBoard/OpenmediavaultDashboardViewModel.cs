using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using Doods.Framework.ApiClientBase.Std.Exceptions;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Std;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem;
using Doods.Openmediavault.Rpc.Std.Data.V5;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.HostManager;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultUpdates;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard
{
    public class OpenmediavaultDashboardViewModel : ViewModelWhithState
    {
        private readonly IOmvService _OmvService;
        private OMVInformations _OMVInformations;
        private string _text = string.Empty;
        private IMessagingCenter _messagingCenter;
        public OpenmediavaultDashboardViewModel(IOmvService omvService, IConfiguration configuration, IMessagingCenter messagingCenter)
        {
            _OmvService = omvService;
            UpdatesCmd = new Command(async(obj)=> await Updates(obj));
            CheckCmd = new Command(async (obj) => await Check(obj));
            ManageHostsCmd = new Command(ManageHosts);
            ChangeHostCmd = new Command(async (obj) => await ChangeHost());
            ShowDetailsCmd = new Command(ShowDetails);
            BannerId = configuration.AdsKey;
            _messagingCenter = messagingCenter;
        }

        //public ObservableRangeCollection<SystemInformation> SystemInformation { get; } =
        //    new ObservableRangeCollection<SystemInformation>();
        public string BannerId { get; }

        public ICommand ManageHostsCmd { get; }
        public ICommand ChangeHostCmd { get; }
        public ICommand ShowDetailsCmd { get; }
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

        private Task ChangeHost()
        {
            var connctionService = App.Container.Resolve<IConnctionService>();
            return connctionService.ChangeHostTask();
        }

        private void ManageHosts()
        {
            NavigationService.NavigateAsync(nameof(HostManagerPage));
        }

        private  Task Check(object obj)
        {
            return ViewModelStateItem.RunActionAsync(async () =>
                {
                    var filename = await _OmvService.UpdateAptList();

                    await Task.Delay(TimeSpan.FromSeconds(3));
                    await CheckRunningAsync(filename);
                }, () => SetLabelsStateItem(openmediavault.Updates, openmediavault.CheckingForNewUpdates___),
                () => SetLabelsStateItem(openmediavault.Updates, openmediavault.Done___));
        }


        private async Task<bool> CheckRunningAsync(string filename)
        {
            if (!await _OmvService.IsRunning(filename)) return false;

            await Task.Delay(TimeSpan.FromSeconds(3));
            return await CheckRunningAsync(filename);
        }

        private async Task<bool> GetOutputAsync(string filename, int pos)
        {
            var result = await _OmvService.GetOutput<string>(filename, pos);
            if (result.Running)
            {
                _text += result.Content;
                await Task.Delay(TimeSpan.FromSeconds(3));
                return await GetOutputAsync(filename, result.Pos);
            }

            return false;
        }

        private void ShowDetails(object obj)
        {
            NavigationService.NavigateAsync(nameof(OpenmediavaultUpdatesPage));
        }

        private Task Updates(object obj)
        {
            _text = string.Empty;
            return ViewModelStateItem.RunActionAsync(async () =>
                {
                    var filename = await _OmvService.UpgradeAptList(Upgradeds.Select(u => u.Name));

                    await Task.Delay(TimeSpan.FromSeconds(3));
                    await GetOutputAsync(filename, 0);
                    await GetUpgraded();
                }, () => SetLabelsStateItem(openmediavault.Updates, $"Upgrading {Upgradeds.Count} pakages"),
                () => { SetLabelsStateItem(openmediavault.Updates, openmediavault.Done___); });
        }

        protected override Task OnInternalDisappearingAsync()
        {
            Shell.SetTabBarIsVisible(Shell.Current.CurrentItem, true);
            return base.OnInternalDisappearingAsync();
        }

        private bool retry = true;
        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                    () => SetLabelsStateItem("OMV", openmediavault.SystemInformation),
                    () =>
                    {
                        UpdateHistory();

                        if (OMVInformations != null && OMVInformations.RebootRequired)
                            SetLabelsStateItem("OMV", openmediavault.TheSystemMustBeRebooted_);
                        else if (OMVInformations != null && OMVInformations.ConfigDirty)
                            SetLabelsStateItem("OMV",
                                openmediavault
                                    .TheConfigurationHasBeenChanged_YouMustApplyTheChangesInOrderForThemToTakeEffect_);
                        else
                            SetLabelsStateItem("OMV", openmediavault.Done___);
                    });
            }
            catch (AuthorizationException ex)
            {
                if (!retry)
                {
                    SetLabelsStateItem(openmediavault.Error, ex.Message);
                    _messagingCenter.Send("this", MessengerKeys.SessionExpired, "item");
                }
                else
                {
                    retry = false;
                    await Task.Delay(100);
                    await OnInternalAppearingAsync();
                }
            }
            catch (Exception e)
            {
                SetLabelsStateItem(openmediavault.Error, e.Message);
            }

            //Shell.SetTabBarIsVisible(Shell.Current.CurrentItem, true);
            await base.OnInternalAppearingAsync();
            retry = true;
        }

        protected Task RefreshData()
        {
            return Task.WhenAll(GetSystemInformation(), GetServicesStatus(), GetUpgraded(), GetFilesystems(),
                GetDevices());
        }

        private async Task GetUpgraded()
        {
            var result = await _OmvService.GetUpgraded();
            Upgradeds.ReplaceRange(result);
        }

        private async Task GetServicesStatus()
        {
            var result = await _OmvService.GetServicesStatus();
            ServicesStatus.ReplaceRange(result);
        }

        private async Task GetFilesystems()
        {
            var result = await _OmvService.GetFilesystems();
            Filesystems.ReplaceRange(result);
        }

        private async Task GetDevices()
        {
            var result = await _OmvService.GetDevices();
            Devices.ReplaceRange(result);
        }


        private async Task GetSystemInformation()
        {
            var result = await _OmvService.GetSystemInformations();
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


        private void UpdateHistory()
        {
            var historyService = App.Container.Resolve<IHistoryService>();
            historyService.CurrentHistoryItem.NombrerPackargeCanBeUpdted = Upgradeds.Count();
            historyService.CurrentHistoryItem.LastUpdate = DateTime.Now;
            historyService.SetHistoryAsync(historyService.CurrentHistoryItem.HostId, historyService.CurrentHistoryItem);
        }
        //protected override void OnFinishLoading(LoadingContext context)
        //{

        //}
    }
}