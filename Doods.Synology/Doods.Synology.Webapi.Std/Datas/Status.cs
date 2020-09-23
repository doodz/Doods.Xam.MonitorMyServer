using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Status
    {
        [JsonProperty("system_crashed")]
        public bool SystemCrashed { get; set; }

        [JsonProperty("system_need_repair")]
        public bool SystemNeedRepair { get; set; }
    }
}