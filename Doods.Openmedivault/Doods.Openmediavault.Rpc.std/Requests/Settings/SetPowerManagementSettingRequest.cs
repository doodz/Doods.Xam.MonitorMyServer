using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;

namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
{
    public class SetPowerManagementSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc PowerMgmt set ";

        public SetPowerManagementSettingRequest(PowerManagementSetting NetworkSetting) : base(
            _commandText +
            $"\"{NetworkSetting.ToJson(true)}\"")
        {
        }
    }
}