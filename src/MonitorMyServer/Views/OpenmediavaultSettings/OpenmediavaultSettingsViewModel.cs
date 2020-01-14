using System;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Input;
using Doods.Framework.Std;
using Doods.Framework.Std.Lists;
using Doods.Framework.Std.Services;
using Doods.Openmediavault.Mobile.Std.Resources;
using Doods.Openmedivault.Ssh.Std.Data;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;
using Xamarin.Forms;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public class OpenmediavaultSettingsViewModel : ViewModelWhithState
    {
        private readonly IOMVSshService _sshService;


       


        public OpenmediavaultSettingsViewModel(IOMVSshService sshService)
        {
            _sshService = sshService;
        }

      

       


        protected override async Task OnInternalAppearingAsync()
        {
            ViewModelStateItem.RunAction(async () => { await RefreshData(); });
            await base.OnInternalAppearingAsync();
        }

        protected Task RefreshData()
        {
            return Task.WhenAll(WebGuiSettings.GetSettings(), PowerManagementSettings.GetSettings(),
                NetworkSettings.GetSettings(),
                DateAndTimeSettings.GetSettings(), AptSettings.GetSettings());
        }
        public AptSettingViewModel AptSettings { get; } = new AptSettingViewModel();
        public WebGuiSettingViewModel WebGuiSettings { get; } = new WebGuiSettingViewModel();
        public PowerManagementSettingViewModel PowerManagementSettings { get; } = new PowerManagementSettingViewModel();
        public NetworkSettingViewModel NetworkSettings { get; } = new NetworkSettingViewModel();
        public TimeSettingViewModel DateAndTimeSettings { get; } = new TimeSettingViewModel();

       
    }
}