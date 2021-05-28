using System.Collections.Generic;

namespace Doods.Openmediavault.Rpc.Std.Requests.UpdatesAndPlugins
{
    public class InstallPlugin : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Plugin install";

        public InstallPlugin(IEnumerable<string> lst) : base(
            $"omv-rpc Plugin install \"{{\\\"packages\\\":[\\\"{string.Join("\\\",\\\"", lst)}\\\"]}}\"")
        {
        }
    }
}