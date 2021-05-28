using System.Threading.Tasks;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultUpdates
{
    internal class OpenmediavaultUpdatesViewModel : ViewModelWhithState
    {
        private readonly IMessageBoxService _messageBoxService;

        private readonly IOmvService _sshService;

        public OpenmediavaultUpdatesViewModel(IOmvService sshService, IMessageBoxService messageBoxService)
        {
            _sshService = sshService;
            _messageBoxService = messageBoxService;
        }

        public ObservableRangeCollection<Upgraded> Upgradeds { get; } =
            new ObservableRangeCollection<Upgraded>();

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