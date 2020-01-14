using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Mobile.Std.controls;
using Doods.Framework.Mobile.Std.Enum;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Lists;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Openmedivault.Ssh.Std.Data.V5;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.AddCustomCommand;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings;
using FFImageLoading.Svg.Forms;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard
{
    public class OpenmediavaultDashboardViewModel : ViewModelWhithState
    {
        private IOMVSshService _sshService;
        public ICommand UpdatesCmd { get; }
        public ICommand CheckCmd { get; }

        //public ObservableRangeCollection<SystemInformation> SystemInformation { get; } =
        //    new ObservableRangeCollection<SystemInformation>();

        private OMVInformations _OMVInformations;
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

        public OpenmediavaultDashboardViewModel(IOMVSshService sshService)
        {
            _sshService = sshService;
            UpdatesCmd = new Command(Updates);
            CheckCmd = new Command(Check);
            
           
        }

        private async void Check(object obj)
        {
           
              await ViewModelStateItem.RunActionAsync(async () =>
            {
                var filename = await _sshService.UpdateAptList();

                await Task.Delay(TimeSpan.FromSeconds(3));
                await CheckRunningAsync(filename);
            }, () => SetLabelsStateItem("verif", "Check update"), () => SetLabelsStateItem("verif", "done!"));
        }


        private async Task<bool> CheckRunningAsync(string filename)
        {
            if (!await _sshService.IsRunning(filename)) return false;

            await Task.Delay(TimeSpan.FromSeconds(3));
            return await CheckRunningAsync(filename);
        }

        private async Task<bool> GetOutputAsync(string filename, long pos)
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

        private string _text = string.Empty;

        private async void Updates(object obj)
        {
            _text = string.Empty;
            await ViewModelStateItem.RunActionAsync(async () =>
                {
                    var filename = await _sshService.UpgradeAptList(Upgradeds.Select(u => u.Name));

                    await Task.Delay(TimeSpan.FromSeconds(3));
                    await GetOutputAsync(filename, 0);
                    await GetUpgraded();
                }, () => SetLabelsStateItem("Apt", $"Upgrading {Upgradeds.Count} pakages"),
                () => { SetLabelsStateItem("Apt", "done!"); });
        }


        protected override async Task OnInternalAppearingAsync()
        {
            ViewModelStateItem.RunAction(async () => { await RefreshData(); });
            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData(bool b = true)
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
            var image2 = new SvgCachedImage
            {
                Source = SvgIconTarget.AddBox.ResourceFile,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 100,
                DownsampleToViewSize = true,
                Aspect = Aspect.AspectFill,
                TransformPlaceholders = false,
                LoadingPlaceholder = "loading.png",
                ErrorPlaceholder = "error.png"
            };


            var image3 = new FileImageSource();
            image3.File = image1;

            yield return new CommandItem(CommandId.AnalyseThematique)
            {
                Text = "Settings",
                IsPrimary = true,
                Command = new Command(GotoSetting),
                Icon = image3
            };
        }
    }
}