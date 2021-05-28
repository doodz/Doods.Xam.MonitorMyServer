using Doods.Openmediavault.Rpc.Std.Interfaces;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V4
{
    public class OMVError : IOmvObject
    {
        [JsonProperty("code")] public long Code { get; set; }

        [JsonProperty("message")] public string Message { get; set; }

        [JsonProperty("trace")] public string Trace { get; set; }
    }
}