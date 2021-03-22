using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Batchtask
    {
        [JsonProperty("max_task")] public long MaxTask { get; set; }

        [JsonProperty("remain_task")] public long RemainTask { get; set; }
    }
}