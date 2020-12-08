using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Synology.Webapi.Std;
using Doods.Synology.Webapi.Std.Datas;
using Doods.Xam.MonitorMyServer.Data.Nas;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;

namespace Doods.Xam.MonitorMyServer.Views.NAS.SharedFolders
{
    public class SharedFoldersViewModel : ViewModelWhithState
    {
        private readonly INasService _nasService;
        private IEnumerable<SharedFolder> _sharedFolders;
        public IEnumerable<SharedFolder> SharedFolders
        {
            get => _sharedFolders;
            set => SetProperty(ref _sharedFolders, value);
        }
        public SharedFoldersViewModel(INasService nasService)
        {
            _nasService = nasService;
        }

        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                    () => SetLabelsStateItem("Nas", openmediavault.SharedFolders),
                    () =>
                    {
                       
                        SetLabelsStateItem("Nas", openmediavault.Done___);
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

            return Task.WhenAll(GetSharedFolders());
        }

        private async Task GetSharedFolders()
        {
            var result = await _nasService.GetSharedFolders();
            SharedFolders = result;
        }
    }
}
