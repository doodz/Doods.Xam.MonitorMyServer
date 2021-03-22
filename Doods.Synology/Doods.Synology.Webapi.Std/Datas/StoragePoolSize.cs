using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class StoragePoolSize
    {
        [JsonProperty("total")] public string Total { get; set; }

        [JsonProperty("used")] public string Used { get; set; }
    }
}