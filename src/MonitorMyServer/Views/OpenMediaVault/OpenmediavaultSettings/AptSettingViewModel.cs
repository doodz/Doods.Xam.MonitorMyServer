using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4.Settings;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public class AptSettingViewModel : BaseSettingsViewModel<AptSetting>
    {
        private bool _partner;

        private bool _proposed;

        public bool Partner
        {
            get => _partner;
            set => SetProperty(ref _partner, value);
        }

        public bool Proposed
        {
            get => _proposed;
            set => SetProperty(ref _proposed, value);
        }

        public override async Task<bool> SaveSettings()
        {
            var obj = new AptSetting();
            obj.Partner = Partner;
            obj.Proposed = Proposed;
            var result = await SshService.SetAptSettings(obj);
            return result;
        }

        public override async Task<AptSetting> GetSettings()
        {
            var obj = await SshService.GetAptSettings();
            Partner = obj.Partner;
            Proposed = obj.Proposed;
            Settings = obj;
            return obj;
        }
    }
}