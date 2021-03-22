using System.Collections.Generic;
using Doods.Webmin.Webapi.Std.Classes;
using Newtonsoft.Json;

namespace Doods.Webmin.Webapi.Std.Datas
{
    public class Stats
    {
        [JsonProperty("proc")] public long Proc { get; set; }

        [JsonProperty("disk")] public List<DiskElement> Disk { get; set; }

        [JsonProperty("net")]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public List<long> Net { get; set; }

        [JsonProperty("mem")] public List<DiskElement> Mem { get; set; }

        [JsonProperty("cpu")] public List<DiskElement> Cpu { get; set; }

        [JsonProperty("scached")] public Scached Scached { get; set; }

        [JsonProperty("virt")] public List<DiskElement> Virt { get; set; }

        [JsonProperty("io")]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public List<long> Io { get; set; }
    }
}