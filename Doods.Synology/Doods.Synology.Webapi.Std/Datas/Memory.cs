using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Memory
    {
        [JsonProperty("avail_real")]
        public long AvailReal { get; set; }

        [JsonProperty("avail_swap")]
        public long AvailSwap { get; set; }

        [JsonProperty("buffer")]
        public long Buffer { get; set; }

        [JsonProperty("cached")]
        public long Cached { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("memory_size")]
        public long MemorySize { get; set; }

        [JsonProperty("real_usage")]
        public long RealUsage { get; set; }

        [JsonProperty("si_disk")]
        public long SiDisk { get; set; }

        [JsonProperty("so_disk")]
        public long SoDisk { get; set; }

        [JsonProperty("swap_usage")]
        public long SwapUsage { get; set; }

        [JsonProperty("total_real")]
        public long TotalReal { get; set; }

        [JsonProperty("total_swap")]
        public long TotalSwap { get; set; }
    }
}