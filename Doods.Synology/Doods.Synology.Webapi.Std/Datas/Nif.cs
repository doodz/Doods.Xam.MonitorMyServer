using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class Nif
    {
        [JsonProperty("addr")]
        public string Addr { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("ipv6")]
        public List<Ipv6> Ipv6 { get; set; }

        [JsonProperty("mac")]
        public string Mac { get; set; }

        [JsonProperty("mask")]
        public string Mask { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}