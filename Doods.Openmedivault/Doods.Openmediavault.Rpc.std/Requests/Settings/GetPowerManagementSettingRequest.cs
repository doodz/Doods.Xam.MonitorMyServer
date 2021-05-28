namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
{
    public class GetPowerManagementSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc PowerMgmt get";

        public GetPowerManagementSettingRequest() : base(_commandText)
        {
        }
    }
}