using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class Ipv6
    {
        [JsonProperty("addr")] public string Addr { get; set; }

        [JsonProperty("prefix_len")] public long PrefixLen { get; set; }

        [JsonProperty("scope")] public string Scope { get; set; }
    }
}