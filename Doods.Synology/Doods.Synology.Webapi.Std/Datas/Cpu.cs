using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Cpu
    {
        [JsonProperty("15min_load")] public long The15MinLoad { get; set; }

        [JsonProperty("1min_load")] public long The1MinLoad { get; set; }

        [JsonProperty("5min_load")] public long The5MinLoad { get; set; }

        [JsonProperty("device")] public string Device { get; set; }

        [JsonProperty("other_load")] public long OtherLoad { get; set; }

        [JsonProperty("system_load")] public long SystemLoad { get; set; }

        [JsonProperty("user_load")] public long UserLoad { get; set; }
    }
}