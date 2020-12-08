using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Proc
    {
        [JsonProperty("cpuFraction")]
        public long CpuFraction { get; set; }

        [JsonProperty("cpu_time")]
        public long CpuTime { get; set; }

        [JsonProperty("memory")]
        public long Memory { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("pid")]
        public long Pid { get; set; }

        [JsonProperty("read")]
        public long Read { get; set; }

        [JsonProperty("write")]
        public long Write { get; set; }
    }
}