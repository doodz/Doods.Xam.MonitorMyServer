using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Flashcache
    {
        [JsonProperty("apply")] public Resize Apply { get; set; }

        [JsonProperty("remove")] public Resize Remove { get; set; }

        [JsonProperty("resize")] public Resize Resize { get; set; }
    }
}