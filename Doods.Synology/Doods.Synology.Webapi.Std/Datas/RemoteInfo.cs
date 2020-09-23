using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class RemoteInfo
    {
        [JsonProperty("compatibility")]
        public string Compatibility { get; set; }

        [JsonProperty("unc")]
        public long Unc { get; set; }
    }
}