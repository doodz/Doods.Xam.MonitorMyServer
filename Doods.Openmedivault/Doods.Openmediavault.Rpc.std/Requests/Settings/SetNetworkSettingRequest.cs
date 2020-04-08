using Doods.Openmediavault.Rpc.std.Data.V4.Settings;
using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SetNetworkSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc Network setGeneralSettings ";

        public SetNetworkSettingRequest(NetworkSetting networkSetting) : base(
            _commandText +
            $"\"{networkSetting.ToJson(true)}\"")
        {
        }
    }
}