using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Disks
    {
        [JsonProperty("disk")]
        public List<Total> DiskDisk { get; set; }

        [JsonProperty("total")]
        public Total Total { get; set; }
    }
}