using Doods.Synology.Webapi.Std.Classes;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Progress
    {
        [JsonProperty("percent")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Percent { get; set; }

        [JsonProperty("step")] public string Step { get; set; }
    }
}