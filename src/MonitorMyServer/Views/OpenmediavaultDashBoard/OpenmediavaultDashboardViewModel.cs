using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Repository.Std.Tables;
using Doods.Framework.Std.Lists;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultDashBoard
{
    public class OpenmediavaultDashboardViewModel : ViewModelWhithState
    {
        private IOMVSshService _sshService;
        public ICommand UpdatesCmd { get; }
        public ICommand CheckCmd { get; }

        public ObservableRangeCollection<SystemInformation> SystemInformation { get; } =
            new ObservableRangeCollection<SystemInformation>();

        public ObservableRangeCollection<Filesystems> Filesystems { get; } =
            new ObservableRangeCollection<Filesystems>();

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
            if (await _sshService.IsRunning(filename)) return true;

            await Task.Delay(TimeSpan.FromSeconds(3));
            return await CheckRunningAsync(filename);
        }


        private void Updates(object obj)
        {
           
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
            var result = await _sshService.GetSystemInformation();
            SystemInformation.ReplaceRange(result);
        }
    }
}