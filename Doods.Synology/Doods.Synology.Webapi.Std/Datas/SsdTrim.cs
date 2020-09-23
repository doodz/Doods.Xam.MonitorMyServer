using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SsdTrim
    {
        [JsonProperty("support")]
        public string Support { get; set; }
    }
}