using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class SynologyUpgradeStatus
    {
        [JsonProperty("allow_upgrade")] public bool AllowUpgrade { get; set; }

        [JsonProperty("status")] public string Status { get; set; }
    }
}