using System.Threading.Tasks;
using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public class PowerManagementSettingViewModel : BaseSettingsViewModel<PowerManagementSetting>
    {
        private PowerbtnAction _selectedPowerbtnAction;
        private bool _cpufreq;
        public bool Cpufreq
        {
            get => _cpufreq;
            set => SetProperty(ref _cpufreq, value);
        }

        public PowerbtnAction SelectedPowerbtnAction
        {
            get => _selectedPowerbtnAction;
            set => SetProperty(ref _selectedPowerbtnAction, value);
        }
        public override async Task<bool> SaveSettings()
        {
            var obj = new PowerManagementSetting();
            obj.Cpufreq = Cpufreq;
            obj.Powerbtn = SelectedPowerbtnAction;
            var result = await SshService.SetPowerManagementSetting(obj);
            return result;
        }

        public override async Task<PowerManagementSetting> GetSettings()
        {
            var obj = await SshService.GetPowerManagementSetting();

            Cpufreq = obj.Cpufreq;
            SelectedPowerbtnAction = obj.Powerbtn;
            return obj;
        }
    }
}