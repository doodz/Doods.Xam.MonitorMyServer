using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class BaseOmvFilesystems : OmvObject
    {
        [JsonProperty("devicefile")]
        public string Devicefile { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}