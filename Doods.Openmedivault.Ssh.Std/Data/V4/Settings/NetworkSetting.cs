using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class NetworkSetting : OmvObject
    {
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [JsonProperty("domainname")]
        public string Domainname { get; set; }

       
    }
}