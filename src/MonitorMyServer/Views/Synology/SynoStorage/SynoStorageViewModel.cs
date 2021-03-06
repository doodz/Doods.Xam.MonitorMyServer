﻿using System;
using System.Threading.Tasks;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Synology.Webapi.Std.Datas;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;

namespace Doods.Xam.MonitorMyServer.Views.Synology.SynoStorage
{
    public class SynologyStorageViewmodel : ViewModelWhithState
    {
        private readonly ISynologyCgiService _synoService;
        private SynologyStorageInfo _synologyStorageInfo;

        public SynologyStorageViewmodel(ISynologyCgiService synoService)
        {
            _synoService = synoService;
        }

        public SynologyStorageInfo SynologyStorageInfo
        {
            get => _synologyStorageInfo;
            set => SetProperty(ref _synologyStorageInfo, value);
        }

        protected override async Task OnInternalAppearingAsync()
        {
            try
            {
                await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                    () => SetLabelsStateItem("Syno", openmediavault.SystemInformation),
                    () => { SetLabelsStateItem("Syno", openmediavault.Done___); });
            }
            catch (Exception e)
            {
                SetLabelsStateItem("Error", e.Message);
            }

            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData()
        {
            return Task.WhenAll(GetSystemInfo());
        }

        private async Task GetSystemInfo()
        {
            var result = await _synoService.GetFullStorageInfo();
            SynologyStorageInfo = result;
        }
    }
}