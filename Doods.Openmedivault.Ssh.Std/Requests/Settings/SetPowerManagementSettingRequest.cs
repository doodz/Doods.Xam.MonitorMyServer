using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SetPowerManagementSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc PowerMgmt set ";

        public SetPowerManagementSettingRequest(PowerManagementSetting NetworkSetting) : base(
            _commandText +
            $"\"{NetworkSetting.ToJson(true)}\"")
        {
        }
    }



   
}