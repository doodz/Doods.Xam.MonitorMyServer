using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data.V5
{
    public class OMVInformations : IOmvObject
    {
        [JsonProperty("ts")] public long Ts { get; set; }

        [JsonProperty("time")] public string Time { get; set; }

        [JsonProperty("hostname")] public string Hostname { get; set; }

        [JsonProperty("version")] public string Version { get; set; }

        [JsonProperty("cpuModelName")] public string CpuModelName { get; set; }

        [JsonProperty("cpuUsage")] public double CpuUsage { get; set; }

        [JsonProperty("memTotal")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MemTotal { get; set; }

        [JsonProperty("memUsed")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MemUsed { get; set; }

        [JsonProperty("kernel")] public string Kernel { get; set; }

        [JsonProperty("uptime")] public string Uptime { get; set; }

        [JsonProperty("loadAverage")] public string LoadAverage { get; set; }

        [JsonProperty("configDirty")] public bool ConfigDirty { get; set; }

        [JsonProperty("rebootRequired")] public bool RebootRequired { get; set; }

        [JsonProperty("pkgUpdatesAvailable")] public bool PkgUpdatesAvailable { get; set; }

        [JsonIgnore] public bool LegacyMode = false;
    }
}