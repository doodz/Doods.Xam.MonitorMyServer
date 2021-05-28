using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V4.Settings
{
    public class AptSetting : OmvObject
    {
        [JsonProperty("proposed")] public bool Proposed { get; set; }

        [JsonProperty("partner")] public bool Partner { get; set; }
    }
}