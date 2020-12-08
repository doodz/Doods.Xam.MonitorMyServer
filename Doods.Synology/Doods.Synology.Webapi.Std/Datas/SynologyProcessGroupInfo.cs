using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyProcessGroupInfo
    {
        [JsonProperty("cgroups")]
        public Dictionary<string, Cgroup> Cgroups { get; set; }

        [JsonProperty("nproc")]
        public long Nproc { get; set; }

        [JsonProperty("procs")]
        public Dictionary<string, Proc> Procs { get; set; }
    }
}