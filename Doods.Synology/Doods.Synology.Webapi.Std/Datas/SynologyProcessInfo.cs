using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyProcessInfo
    {
        [JsonProperty("process")] public List<Process> Process { get; set; }
    }
}