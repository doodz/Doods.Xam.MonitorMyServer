using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.std.Data.V4
{
    public class IsRunning
    {

            [JsonProperty("filename")]
            public string Filename { get; set; }

            [JsonProperty("running")]
            public bool Running { get; set; }
        
    }
}
