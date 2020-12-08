using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Total
    {
        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayName { get; set; }

        [JsonProperty("read_access")]
        public long ReadAccess { get; set; }

        [JsonProperty("read_byte")]
        public long ReadByte { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("utilization")]
        public long Utilization { get; set; }

        [JsonProperty("write_access")]
        public long WriteAccess { get; set; }

        [JsonProperty("write_byte")]
        public long WriteByte { get; set; }
    }
}