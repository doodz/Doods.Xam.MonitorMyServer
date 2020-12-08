using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class ServiceAdditional
    {
        [JsonProperty("allow_control", NullValueHandling = NullValueHandling.Ignore)]
        public bool? AllowControl { get; set; }

        [JsonProperty("status")]
        public FluffyStatus Status { get; set; }
    }
}