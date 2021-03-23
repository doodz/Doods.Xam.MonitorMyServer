using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Device
    {
        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("slot")] public long Slot { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
}