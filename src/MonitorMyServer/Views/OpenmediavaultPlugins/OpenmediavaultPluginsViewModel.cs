using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Std.Extensions;
using Doods.Framework.Std.Lists;
using Doods.Openmediavault.Mobile.Std.Models;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Doods.Xam.MonitorMyServer.Views.OpenmediavaultFileSystems.OpenmediavaultAddFileSystem;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultPlugins
{
    public class OpenmediavaultPluginsViewModel : ViewModelWhithState
    {

        public Command InstallRemoveCmd { get; }

        private readonly IOMVSshService _sshService;
        public ICommand OmvPluginsChangedCmd { get; }
        public ObservableRangeCollection<PluginInfo> Plugins { get; } =
            new ObservableRangeCollection<PluginInfo>();

        public OpenmediavaultPluginsViewModel(IOMVSshService sshService)
        {
            _sshService = sshService;
            OmvPluginsChangedCmd = new Command(OmvPluginsChanged);
            InstallRemoveCmd = new Command(InstallRemove,CanExecute);
        }

        private bool CanExecute(object arg)
        {
            return _actionList.Any();
        }

        private void InstallRemove(object obj)
        {
            var install = _actionList.Where(o => !o.Installed);
            var remove = _actionList.Where(o => o.Installed);

        }

       private ICollection<PluginInfo> _actionList =new List<PluginInfo>();

        private void OmvPluginsChanged(object obj)
        {
            if (obj is PluginInfo pluging)
            {
                if (pluging.IsSelected)
                {
                    _actionList.Add(pluging);
                }
                else
                {
                    _actionList.Remove(pluging);
                }
                InstallRemoveCmd.ChangeCanExecute();
            }
        }

        protected override async Task OnInternalAppearingAsync()
        {
            await ViewModelStateItem.RunActionAsync(async () => { await RefreshData(); },
                () => SetLabelsStateItem("OMV", $"Get Plugins"),
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
            if(result != null)
                Plugins.ReplaceRange(result);
        }
    }
}
