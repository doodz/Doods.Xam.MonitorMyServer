using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class CanDo
    {
        [JsonProperty("delete")] public bool Delete { get; set; }

        [JsonProperty("migrate", NullValueHandling = NullValueHandling.Ignore)]
        public Migrate Migrate { get; set; }

        [JsonProperty("raid_cross")] public bool RaidCross { get; set; }
    }
}