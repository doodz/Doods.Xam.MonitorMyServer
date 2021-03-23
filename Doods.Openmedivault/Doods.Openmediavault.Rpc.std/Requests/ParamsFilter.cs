using Doods.Openmediavault.Rpc.std.Data.V4;
using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class ParamsFilter : OmvObject
    {
        [JsonProperty("start")] public long Start { get; set; }

        [JsonProperty("limit")] public long Limit { get; set; } = 25;

        [JsonProperty("sortfield")] public string Sortfield { get; set; }

        [JsonProperty("sortdir")] public string Sortdir { get; set; } = "ASC";
    }
}