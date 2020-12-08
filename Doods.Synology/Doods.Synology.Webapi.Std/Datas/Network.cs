using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Network
    {
        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("rx")]
        public long Rx { get; set; }

        [JsonProperty("tx")]
        public long Tx { get; set; }
    }
}