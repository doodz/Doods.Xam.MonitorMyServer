using System.Collections.Generic;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class RemovePlugin : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Plugin remove";
        public RemovePlugin(IEnumerable<string> lst) : base($"omv-rpc Plugin remove \"{{\\\"packages\\\":[\\\"{string.Join("\\\",\\\"", lst)}\\\"]}}\"")
        {
        }
    }
}