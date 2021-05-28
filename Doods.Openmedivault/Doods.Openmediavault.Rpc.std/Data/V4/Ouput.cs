using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V4
{
    public class OutputBase
    {
        [JsonProperty("filename")] public string Filename { get; set; }

        [JsonProperty("pos")] public int Pos { get; set; }


        [JsonProperty("running")] public bool Running { get; set; }
    }


    public class Output<T> : OutputBase
    {
        [JsonProperty("output")] public T Content { get; set; }
    }

    public class Output : OutputBase
    {
        [JsonProperty("output")] public string Content { get; set; }
    }
}