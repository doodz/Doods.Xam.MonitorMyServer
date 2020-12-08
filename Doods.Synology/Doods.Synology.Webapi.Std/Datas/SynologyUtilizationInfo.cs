using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyUtilizationInfo
    {
        [JsonProperty("cpu")]
        public Cpu Cpu { get; set; }

        [JsonProperty("disk")]
        public Disks Disk { get; set; }

        [JsonProperty("lun")]
        public List<object> Lun { get; set; }

        [JsonProperty("memory")]
        public Memory Memory { get; set; }

        [JsonProperty("network")]
        public List<Network> Network { get; set; }

        [JsonProperty("space")]
        public Space Space { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }
}