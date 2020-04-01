using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class SynologyApiServicesInfo
    {
        [JsonProperty("maxVersion")]
        public long MaxVersion { get; set; }

        [JsonProperty("minVersion")]
        public long MinVersion { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("requestFormat", NullValueHandling = NullValueHandling.Ignore)]
        public RequestFormat? RequestFormat { get; set; }
    }
}