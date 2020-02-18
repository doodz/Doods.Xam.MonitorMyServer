﻿using Doods.Openmedivault.Ssh.Std.Data;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.std.Data.V4
{
    public class LogLine : IOmvObject
    {
        [JsonProperty("rownum")] public long Rownum { get; set; }

        [JsonProperty("ts")] public long Ts { get; set; }

        [JsonProperty("date")] public string Date { get; set; }

        [JsonProperty("hostname")] public string Hostname { get; set; }

        [JsonProperty("message")] public string Message { get; set; }
    }
}