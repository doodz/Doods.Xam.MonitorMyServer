using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Openmedivault.Ssh.Std.Data;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class GetNetworkSettingRequest : OmvRequestBase
    {
        private static string _commandText = "omv-rpc Network getGeneralSettings";

        public GetNetworkSettingRequest() : base(_commandText)
        {
        }
    }
}