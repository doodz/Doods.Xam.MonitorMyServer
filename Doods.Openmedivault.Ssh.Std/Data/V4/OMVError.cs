using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class OMVError : IOmvObject
    {

        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("trace")]
        public string Trace { get; set; }
    }
}