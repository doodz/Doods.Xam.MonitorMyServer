using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;

namespace Doods.Xam.MonitorMyServer.Views.Linux.DisksUsage
{
    public class DisksUsageViewmodel : ViewModelWhithState
    {
        private readonly ISshService _sshService;
        private IEnumerable<DiskUsage> _disksUsage;
        public IEnumerable<DiskUsage> DisksUsage
        {
            get => _disksUsage;
            set => SetProperty(ref _disksUsage, value);
        }
        public DisksUsageViewmodel(ISshService sshService)
        {
            _sshService = sshService;
        }

        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                    () => SetLabelsStateItem("Bash", openmediavault.SystemInformation),
                    () =>
                    {
                       
                        SetLabelsStateItem("Bash", openmediavault.Done___);
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

            return Task.WhenAll(GetDisksUsage());
        }

        private async Task GetDisksUsage()
        {
            DisksUsage = await _sshService.GetDisksUsage();
        }
    }
}
