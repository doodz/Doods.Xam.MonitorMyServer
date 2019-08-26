using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class Disk : IOmvObject
    {
        [JsonProperty("devicename")]
        public string Devicename { get; set; }

        [JsonProperty("devicefile")]
        public string Devicefile { get; set; }

        [JsonProperty("devicelinks")]
        public List<string> Devicelinks { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("vendor")]
        public string Vendor { get; set; }

        [JsonProperty("serialnumber")]
        public string Serialnumber { get; set; }

        [JsonProperty("israid")]
        public bool Israid { get; set; }

        [JsonProperty("isroot")]
        public bool Isroot { get; set; }
    }
}