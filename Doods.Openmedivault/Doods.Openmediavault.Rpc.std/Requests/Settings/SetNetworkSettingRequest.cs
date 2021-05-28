using Doods.Openmediavault.Rpc.Std.Data.V4.Settings;

namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
{
    public class SetNetworkSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc Network setGeneralSettings ";

        public SetNetworkSettingRequest(NetworkSetting networkSetting) : base(
            _commandText +
            $"\"{networkSetting.ToJson(true)}\"")
        {
        }
    }
}