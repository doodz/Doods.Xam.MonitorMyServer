using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Process
    {
        [JsonProperty("command")] public string Command { get; set; }

        [JsonProperty("cpu")] public long Cpu { get; set; }

        [JsonProperty("mem")] public long Mem { get; set; }

        [JsonProperty("mem_shared")] public long MemShared { get; set; }

        [JsonProperty("pid")] public long Pid { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
}