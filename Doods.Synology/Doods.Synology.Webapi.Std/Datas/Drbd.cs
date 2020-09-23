using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Drbd
    {
        [JsonProperty("resize")]
        public Resize Resize { get; set; }
    }
}