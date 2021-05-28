using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V4.Settings
{
    public class WebGuiSetting : OmvObject
    {
        [JsonProperty("port")] public long Port { get; set; }

        [JsonProperty("enablessl")] public bool Enablessl { get; set; }

        [JsonProperty("sslport")] public long Sslport { get; set; }

        [JsonProperty("forcesslonly")] public bool Forcesslonly { get; set; }

        [JsonProperty("sslcertificateref")] public string Sslcertificateref { get; set; }

        [JsonProperty("timeout")] public long Timeout { get; set; }
    }
}