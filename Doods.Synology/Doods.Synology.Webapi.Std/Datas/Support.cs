using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Support
    {
        [JsonProperty("ebox")]
        public bool Ebox { get; set; }

        [JsonProperty("raid_cross")]
        public bool RaidCross { get; set; }

        [JsonProperty("sysdef")]
        public bool Sysdef { get; set; }
    }
}