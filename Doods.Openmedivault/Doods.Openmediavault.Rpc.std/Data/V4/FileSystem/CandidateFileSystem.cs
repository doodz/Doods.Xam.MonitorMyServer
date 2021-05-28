using Doods.Openmediavault.Rpc.Std.Interfaces;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V4.FileSystem
{
    public class CandidateFileSystem : IOmvObject
    {
        [JsonProperty("devicefile")] public string Devicefile { get; set; }

        [JsonProperty("size")] public string Size { get; set; }

        [JsonProperty("description")] public string Description { get; set; }
    }
}