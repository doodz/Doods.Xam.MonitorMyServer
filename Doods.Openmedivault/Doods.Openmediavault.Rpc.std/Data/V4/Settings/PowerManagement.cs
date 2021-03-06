﻿using Doods.Openmediavault.Rpc.Std.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Doods.Openmediavault.Rpc.Std.Data.V4.Settings
{
    public class PowerManagementSetting : OmvObject
    {
        [JsonProperty("cpufreq")] public bool Cpufreq { get; set; }

        [JsonProperty("powerbtn")]
        [JsonConverter(typeof(StringEnumConverter), typeof(SnakeCaseNamingStrategy))]
        public PowerbtnAction Powerbtn { get; set; }
    }
}