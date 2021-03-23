using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class VolInfo
    {
        [JsonProperty("desc")] public string Desc { get; set; }

        [JsonProperty("inode_free")] public long InodeFree { get; set; }

        [JsonProperty("inode_total")] public long InodeTotal { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("total_size")] public string TotalSize { get; set; }

        [JsonProperty("used_size")] public string UsedSize { get; set; }

        [JsonProperty("volume")] public string Volume { get; set; }
    }
}