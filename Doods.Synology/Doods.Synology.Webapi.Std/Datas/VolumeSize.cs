using Doods.Synology.Webapi.Std.Classes;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class VolumeSize
    {
        [JsonProperty("free_inode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long FreeInode { get; set; }

        [JsonProperty("total")]
        public string Total { get; set; }

        [JsonProperty("total_device")]
        public string TotalDevice { get; set; }

        [JsonProperty("total_inode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long TotalInode { get; set; }

        [JsonProperty("used")]
        public string Used { get; set; }
    }
}