using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmediavault.Rpc.std.Data.V4.FileSystem;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultUpdates
{
    internal class OpenmediavaultUpdatesViewModel : ViewModelWhithState
    {
        public ObservableRangeCollection<Upgraded> Upgradeds { get; } =
            new ObservableRangeCollection<Upgraded>();
        private readonly IOmvService _sshService;
        private readonly IMessageBoxService _messageBoxService;
        public OpenmediavaultUpdatesViewModel(IOmvService sshService, IMessageBoxService messageBoxService)
        {
            _sshService = sshService;
            _messageBoxService = messageBoxService;

        }

        protected override async Task OnInternalAppearingAsync()
        {
           
            await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                () => SetLabelsStateItem(openmediavault.UpdateManagement, openmediavault.Loading_PleaseWait___),
                () => { SetLabelsStateItem(openmediavault.UpdateManagement, openmediavault.Done___); });
            await base.OnInternalAppearingAsync();
        }

        protected async Task RefreshData()
        {
            await Task.WhenAll(GetListFileSystem());
        }

        private async Task GetListFileSystem()
        {
            var result = await _sshService.GetUpgraded();
           
            Upgradeds.ReplaceRange(result);
        }

    }
}
