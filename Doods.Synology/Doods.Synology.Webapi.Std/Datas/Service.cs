using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Service
    {
        [JsonProperty("additional")]
        public ServiceAdditional Additional { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("enable")]
        public bool Enable { get; set; }

        [JsonProperty("service_id")]
        public string ServiceId { get; set; }
    }
}