using System;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Exceptions;
using Doods.Framework.Mobile.Std.Interfaces;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Openmediavault.Rpc.Std.Data.V4;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultUpdates
{
    internal class OpenmediavaultUpdatesViewModel : ViewModelWhithState
    {
        private readonly IMessageBoxService _messageBoxService;
        private IMessagingCenter _messagingCenter;
        private readonly IOmvService _sshService;

        public OpenmediavaultUpdatesViewModel(IOmvService sshService, IMessageBoxService messageBoxService, IMessagingCenter messagingCenter)
        {
            _sshService = sshService;
            _messageBoxService = messageBoxService;
            _messagingCenter = messagingCenter;
        }

        public ObservableRangeCollection<Upgraded> Upgradeds { get; } =
            new ObservableRangeCollection<Upgraded>();
        private bool retry = true;
        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                () => SetLabelsStateItem(openmediavault.UpdateManagement, openmediavault.Loading_PleaseWait___),
                () => { SetLabelsStateItem(openmediavault.UpdateManagement, openmediavault.Done___); });
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
            await base.OnInternalAppearingAsync();
            retry = true;
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