using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Space
    {
        [JsonProperty("total")] public Total Total { get; set; }

        [JsonProperty("volume")] public List<Total> Volume { get; set; }
    }
}