using System.Threading.Tasks;
using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Xam.MonitorMyServer.Views.OpenmediavaultSettings
{
    public class WebGuiSettingViewModel : BaseSettingsViewModel<WebGuiSetting>
    {
        private bool _enablessl;
        private bool _forcesslonly;
        private long _port;

        private TimePickerEnum _selectedTimeout;
        private string _sslcertificateref;
        private long _sslport;
        private long _timeout;

        public long Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public bool Enablessl
        {
            get => _enablessl;
            set => SetProperty(ref _enablessl, value);
        }

        public long Sslport
        {
            get => _sslport;
            set => SetProperty(ref _sslport, value);
        }

        public bool Forcesslonly
        {
            get => _forcesslonly;
            set => SetProperty(ref _forcesslonly, value);
        }

        public string Sslcertificateref
        {
            get => _sslcertificateref;
            set => SetProperty(ref _sslcertificateref, value);
        }

        public TimePickerEnum SelectedTimeout
        {
            get => _selectedTimeout;
            set => SetProperty(ref _selectedTimeout, value);
        }

        public long Timeout
        {
            get => _timeout;
            set => SetProperty(ref _timeout, value);
        }

        public override async Task<bool> SaveSettings()
        {
            var obj = new WebGuiSetting();
            obj.Port = Port;
            obj.Enablessl = Enablessl;
            obj.Sslport = Sslport;
            obj.Forcesslonly = Forcesslonly;
            obj.Sslcertificateref = Sslcertificateref;
            obj.Timeout = Timeout;
            var result = await SshService.SetWebGuiSettings(obj);
            return result;
        }

        public override async Task<WebGuiSetting> GetSettings()
        {
            var obj = await SshService.GetWebGuiSettings();
            Port = obj.Port;
            Enablessl = obj.Enablessl;
            Sslport = obj.Sslport;
            Forcesslonly = obj.Forcesslonly;
            Sslcertificateref = obj.Sslcertificateref;
            Timeout = obj.Timeout;
            Settings = obj;
            return obj;
        }
    }
}