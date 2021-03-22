using System.Collections.Generic;
using Doods.Synology.Webapi.Std.Classes;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Env
    {
        [JsonProperty("batchtask")] public Batchtask Batchtask { get; set; }

        [JsonProperty("bay_number")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long BayNumber { get; set; }

        [JsonProperty("data_scrubbing")] public DataScrubbing DataScrubbing { get; set; }

        [JsonProperty("ebox")] public List<object> Ebox { get; set; }

        [JsonProperty("fs_acting")] public bool FsActing { get; set; }

        [JsonProperty("isSyncSysPartition")] public bool IsSyncSysPartition { get; set; }

        [JsonProperty("is_space_actioning")] public bool IsSpaceActioning { get; set; }

        [JsonProperty("isns")] public Isns Isns { get; set; }

        [JsonProperty("isns_server")] public string IsnsServer { get; set; }

        [JsonProperty("max_fs_bytes")] public string MaxFsBytes { get; set; }

        [JsonProperty("max_fs_bytes_1PB")] public string MaxFsBytes1Pb { get; set; }

        [JsonProperty("max_fs_bytes_high_end")]
        public string MaxFsBytesHighEnd { get; set; }

        [JsonProperty("max_pool_usable_bytes")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MaxPoolUsableBytes { get; set; }

        [JsonProperty("model_name")] public string ModelName { get; set; }

        [JsonProperty("showpooltab")] public bool Showpooltab { get; set; }

        [JsonProperty("space_size_limit")] public SpaceSizeLimit SpaceSizeLimit { get; set; }

        [JsonProperty("status")] public Status Status { get; set; }

        [JsonProperty("support")] public Support Support { get; set; }

        [JsonProperty("support_fit_fs_limit")] public bool SupportFitFsLimit { get; set; }

        [JsonProperty("unique_key")] public string UniqueKey { get; set; }

        [JsonProperty("volume_full_critical")] public double VolumeFullCritical { get; set; }

        [JsonProperty("volume_full_warning")] public double VolumeFullWarning { get; set; }
    }
}