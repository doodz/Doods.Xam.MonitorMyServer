
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
   

    public class ServicesStatus : IOmvObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("running")]
        public bool Running { get; set; }
    }
}