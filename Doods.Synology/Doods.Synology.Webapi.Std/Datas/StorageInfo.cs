using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class StorageInfo
    {
        [JsonProperty("hdd_info")]
        public List<HddInfo> HddInfo { get; set; }

        [JsonProperty("vol_info")]
        public List<VolInfo> VolInfo { get; set; }
    }
}