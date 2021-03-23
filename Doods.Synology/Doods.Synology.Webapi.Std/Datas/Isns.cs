using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Isns
    {
        [JsonProperty("address")] public string Address { get; set; }

        [JsonProperty("enabled")] public bool Enabled { get; set; }
    }
}