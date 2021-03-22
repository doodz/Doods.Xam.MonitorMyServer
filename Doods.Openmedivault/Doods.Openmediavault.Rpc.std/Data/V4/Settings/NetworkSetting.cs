﻿using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.std.Data.V4.Settings
{
    public class NetworkSetting : OmvObject
    {
        [JsonProperty("hostname")] public string Hostname { get; set; }

        [JsonProperty("domainname")] public string Domainname { get; set; }
    }
}