using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doods.Framework.Mobile.Ssh.Std.Models;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;

namespace Doods.Xam.MonitorMyServer.Views.Linux.DisksUsage
{
    //cat /proc/mdstat
    // mdadm --detail /dev/md0
    public class DisksUsageViewmodel : ViewModelWhithState
    {
        private readonly ISshService _sshService;
        private IEnumerable<DiskUsage> _disksUsage;
        public IEnumerable<DiskUsage> DisksUsage
        {
            get => _disksUsage;
            set => SetProperty(ref _disksUsage, value);
        }

        private IEnumerable<Blockdevice> _blockdevices;
        public IEnumerable<Blockdevice> Blockdevices
        {
            get => _blockdevices;
            set => SetProperty(ref _blockdevices, value);
        }

        private IEnumerable<Blockdevice> _diskdevices;
        public IEnumerable<Blockdevice> Diskdevices
        {
            get => _diskdevices;
            set => SetProperty(ref _diskdevices, value);
        }

        private IEnumerable<Blockdevice> _raiddevices;
        public IEnumerable<Blockdevice> Raiddevices
        {
            get => _raiddevices;
            set => SetProperty(ref _raiddevices, value);
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

            return Task.WhenAll(GetDisksUsage(), GetBlockdevices());
        }

        private async Task GetDisksUsage()
        {
            DisksUsage = await _sshService.GetDisksUsage();
        }
        private async Task GetBlockdevices()
        {
            Blockdevices = await _sshService.GetBlockdevices();
            Diskdevices = Blockdevices?.Where(b => b.Type == Blockdevice.TypeDisk);
            Raiddevices = Blockdevices?.Where(b => b.Type.Contains(Blockdevice.TypeRaid));
        }
      
    }
}
