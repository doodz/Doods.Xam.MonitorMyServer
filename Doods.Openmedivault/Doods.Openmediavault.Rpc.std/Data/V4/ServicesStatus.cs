using Doods.Openmediavault.Rpc.Std.Interfaces;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V4
{
    public class ServicesStatus : IOmvObject
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("enabled")] public bool Enabled { get; set; }

        [JsonProperty("running")] public bool Running { get; set; }
    }
}