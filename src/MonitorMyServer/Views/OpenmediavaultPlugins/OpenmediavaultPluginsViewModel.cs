﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Mobile.Std.Models;
using Doods.Openmedivault.Ssh.Std.Requests;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins
{
    public class OpenmediavaultPluginsViewModel : ViewModelWhithState
    {
        private readonly IOmvService _sshService;

        private readonly ICollection<PluginInfo> _actionList = new List<PluginInfo>();

        public OpenmediavaultPluginsViewModel(IOmvService sshService)
        {
            _sshService = sshService;
            OmvPluginsChangedCmd = new Command(OmvPluginsChanged);
            InstallRemoveCmd = new Command(InstallRemove, CanExecute);
        }

        public Command InstallRemoveCmd { get; }
        public ICommand OmvPluginsChangedCmd { get; }

        public ObservableRangeCollection<PluginInfo> Plugins { get; } =
            new ObservableRangeCollection<PluginInfo>();

        private bool CanExecute(object arg)
        {
            return _actionList.Any();
        }

        private void InstallRemove(object obj)
        {
            var install = _actionList.Where(o => !o.Installed);
            var remove = _actionList.Where(o => o.Installed);
        }

        private void OmvPluginsChanged(object obj)
        {
            if (obj is PluginInfo pluging)
            {
                if (pluging.IsSelected)
                    _actionList.Add(pluging);
                else
                    _actionList.Remove(pluging);

                InstallRemoveCmd.ChangeCanExecute();
            }
        }

        protected override async Task OnInternalAppearingAsync()
        {
            await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                () => SetLabelsStateItem("OMV", "Get Plugins"),
                () => { SetLabelsStateItem("OMV", "done!"); });
            await base.OnInternalAppearingAsync();
        }


        protected async Task RefreshData()
        {
            await Task.WhenAll(GetListPlugins());
        }

        private async Task GetListPlugins()
        {
            var result = await _sshService.GetPlugins();
            if (result != null)
                Plugins.ReplaceRange(result);
        }
    }
}