using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Migrate
    {
        [JsonProperty("add_mirror")] public long AddMirror { get; set; }

        [JsonProperty("to_raid5")] public long ToRaid5 { get; set; }

        [JsonProperty("to_raid5+spare")] public string ToRaid5Spare { get; set; }
    }
}