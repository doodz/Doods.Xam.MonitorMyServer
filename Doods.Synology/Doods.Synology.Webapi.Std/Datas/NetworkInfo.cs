using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class NetworkInfo
    {
        [JsonProperty("dns")]
        public string Dns { get; set; }

        [JsonProperty("enabled_domain")]
        public bool EnabledDomain { get; set; }

        [JsonProperty("enabled_samba")]
        public bool EnabledSamba { get; set; }

        [JsonProperty("gateway")]
        public string Gateway { get; set; }

        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("nif")]
        public List<Nif> Nif { get; set; }

        [JsonProperty("wins")]
        public string Wins { get; set; }

        [JsonProperty("workgroup")]
        public string Workgroup { get; set; }
    }
}