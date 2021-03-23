using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Raid
    {
        [JsonProperty("designedDiskCount")] public long DesignedDiskCount { get; set; }

        [JsonProperty("devices")] public List<Device> Devices { get; set; }

        [JsonProperty("hasParity")] public bool HasParity { get; set; }

        [JsonProperty("minDevSize")] public string MinDevSize { get; set; }

        [JsonProperty("normalDevCount")] public long NormalDevCount { get; set; }

        [JsonProperty("raidPath")] public string RaidPath { get; set; }

        [JsonProperty("raidStatus")] public long RaidStatus { get; set; }

        [JsonProperty("spares")] public List<object> Spares { get; set; }
    }
}