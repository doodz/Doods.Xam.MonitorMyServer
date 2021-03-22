using Newtonsoft.Json;

namespace Doods.Webmin.Webapi.Std.Datas
{
    public class ProcElement
    {
        [JsonProperty("y")] public long Y { get; set; }

        [JsonProperty("x")] public long X { get; set; }
    }
}