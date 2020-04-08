using System.Threading.Tasks;
using Doods.Xam.MonitorMyServer.Services;
using Doods.Xam.MonitorMyServer.Views.Base;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public class OpenmediavaultSettingsViewModel : ViewModelWhithState
    {
        private readonly IOmvService _sshService;


        public OpenmediavaultSettingsViewModel(IOmvService sshService)
        {
            _sshService = sshService;
        }

        public AptSettingViewModel AptSettings { get; } = new AptSettingViewModel();
        public WebGuiSettingViewModel WebGuiSettings { get; } = new WebGuiSettingViewModel();
        public PowerManagementSettingViewModel PowerManagementSettings { get; } = new PowerManagementSettingViewModel();
        public NetworkSettingViewModel NetworkSettings { get; } = new NetworkSettingViewModel();
        public TimeSettingViewModel DateAndTimeSettings { get; } = new TimeSettingViewModel();


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
    }
}