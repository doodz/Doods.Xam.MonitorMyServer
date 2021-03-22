namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetNetworkSettingRequest : OmvRequestBase
    {
        private static readonly string _commandText = "omv-rpc Network getGeneralSettings";

        public GetNetworkSettingRequest() : base(_commandText)
        {
        }
    }
}