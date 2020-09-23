using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SpaceSizeLimit
    {
        [JsonProperty("allocatable_size")]
        public long AllocatableSize { get; set; }

        [JsonProperty("is_limited")]
        public bool IsLimited { get; set; }

        [JsonProperty("size_limit")]
        public long SizeLimit { get; set; }
    }
}