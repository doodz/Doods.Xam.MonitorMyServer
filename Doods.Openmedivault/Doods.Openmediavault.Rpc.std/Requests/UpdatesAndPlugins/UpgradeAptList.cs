using System.Collections.Generic;

namespace Doods.Openmediavault.Rpc.Std.Requests.UpdatesAndPlugins
{
    public class UpgradeAptList : OmvRequestBase
    {
        public UpgradeAptList(IEnumerable<string> lst) : base(
            $"omv-rpc Apt upgrade  \"{{\\\"packages\\\":[\\\"{string.Join("\\\",\\\"", lst)}\\\"]}}\"")
        {
            //\\\"{filename}\\\"
            var test = $"omv-rpc Apt upgrade  \"{{\\\"packages\\\":[\\\"{string.Join(",\\\"", lst)}]}}\"";
        }
    }
}