using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;

namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
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