namespace Doods.Openmediavault.Rpc.Std.Requests.Settings
{
    public class GetNetworkSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc Network getGeneralSettings";

        public GetNetworkSettingRequest() : base(_commandText)
        {
        }
    }
}