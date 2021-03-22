using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Webmin.Webapi.Std.Datas
{
    public class Scached
    {
        [JsonProperty("mem")] public List<ProcElement> Mem { get; set; }

        [JsonProperty("net")] public List<Disk> Net { get; set; }

        [JsonProperty("virt")] public List<ProcElement> Virt { get; set; }

        [JsonProperty("cpu")] public List<ProcElement> Cpu { get; set; }

        [JsonProperty("disk")] public List<Disk> Disk { get; set; }

        [JsonProperty("proc")] public List<ProcElement> Proc { get; set; }
    }
}