using Doods.Synology.Webapi.Std.Classes;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class DataScrubbing
    {
        [JsonProperty("sche_enabled")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ScheEnabled { get; set; }

        [JsonProperty("sche_status")] public string ScheStatus { get; set; }
    }
}