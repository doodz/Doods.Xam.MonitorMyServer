using Doods.Openmediavault.Rpc.std.Data.V4.Settings;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SetWebAdministrationSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc WebGui setSettings ";

        public SetWebAdministrationSettingRequest(WebAdministrationSetting webAdministrationSetting) : base(
            _commandText + $"\"{webAdministrationSetting.ToJson(true)}\"")
        {
        }
    }
}