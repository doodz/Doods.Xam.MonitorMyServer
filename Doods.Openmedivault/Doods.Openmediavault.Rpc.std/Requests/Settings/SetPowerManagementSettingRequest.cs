using Doods.Openmediavault.Rpc.std.Data.V4.Settings;

namespace Doods.Openmedivault.Ssh.Std.Requests
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