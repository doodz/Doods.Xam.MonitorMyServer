using System;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V4.SharedFolders
{
    public class SharedFolder : IOmvObject
    {
        [JsonProperty("uuid")] public Guid Uuid { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("comment")] public string Comment { get; set; }

        [JsonProperty("mntentref")] public Guid Mntentref { get; set; }

        [JsonProperty("reldirpath")] public string Reldirpath { get; set; }

        [JsonProperty("privileges")] public string Privileges { get; set; }

        [JsonProperty("_used")] public bool Used { get; set; }

        [JsonProperty("device")] public string Device { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("mntent")] public Mntent Mntent { get; set; }
    }
}