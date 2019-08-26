using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class AptSetting : OmvObject
    {
        [JsonProperty("proposed")]
        public bool Proposed { get; set; }

        [JsonProperty("partner")]
        public bool Partner { get; set; }
       
    }
}