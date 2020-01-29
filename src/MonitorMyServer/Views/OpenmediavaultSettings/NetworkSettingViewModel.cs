using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public class NetworkSettingViewModel : BaseSettingsViewModel<NetworkSetting>
    {
        private string _domainname;

        private string _hostname;
        public string Domainname
        {
            get => _domainname;
            set => SetProperty(ref _domainname, value);
        }



        public string Hostname
        {
            get => _hostname;
            set => SetProperty(ref _hostname, value);
        }

        public override async Task<bool> SaveSettings()
        {
            var obj = new NetworkSetting();
            obj.Domainname = Domainname;
            obj.Hostname = Hostname;
            var result = await SshService.SetNetworkSetting(obj);
            return result;
        }

        public override async Task<NetworkSetting> GetSettings()
        {
            var obj = await SshService.GetNetworkSetting();
            Domainname = obj.Domainname;
            Hostname = obj.Hostname;
            Settings = obj;
            return obj;
        }
    }
}