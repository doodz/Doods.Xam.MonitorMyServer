using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class VspaceCanDo
    {
        [JsonProperty("drbd")]
        public Drbd Drbd { get; set; }

        [JsonProperty("flashcache")]
        public Flashcache Flashcache { get; set; }

        [JsonProperty("snapshot")]
        public Drbd Snapshot { get; set; }
    }
}