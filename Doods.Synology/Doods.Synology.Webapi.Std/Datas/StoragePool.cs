using System.Collections.Generic;
using Doods.Synology.Webapi.Std.Classes;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class StoragePool
    {
        [JsonProperty("cacheStatus")] public string CacheStatus { get; set; }

        [JsonProperty("can_do")] public CanDo CanDo { get; set; }

        [JsonProperty("container")] public string Container { get; set; }

        [JsonProperty("deploy_path")] public string DeployPath { get; set; }

        [JsonProperty("desc")] public string Desc { get; set; }

        [JsonProperty("device_type")] public string DeviceType { get; set; }

        [JsonProperty("disk_failure_number")] public long DiskFailureNumber { get; set; }

        [JsonProperty("disks")] public List<string> Disks { get; set; }

        [JsonProperty("drive_type")] public long DriveType { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("is_actioning")] public bool IsActioning { get; set; }

        [JsonProperty("is_oversize_pool")] public bool IsOversizePool { get; set; }

        [JsonProperty("is_scheduled")] public bool IsScheduled { get; set; }

        [JsonProperty("is_writable")] public bool IsWritable { get; set; }

        [JsonProperty("last_done_time")] public long LastDoneTime { get; set; }

        [JsonProperty("limited_disk_number")] public long LimitedDiskNumber { get; set; }

        [JsonProperty("max_pool_usable_bytes")]
        public string MaxPoolUsableBytes { get; set; }

        [JsonProperty("maximal_disk_size")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MaximalDiskSize { get; set; }

        [JsonProperty("minimal_disk_size")] public string MinimalDiskSize { get; set; }

        [JsonProperty("next_schedule_time")] public long NextScheduleTime { get; set; }

        [JsonProperty("num_id")] public long NumId { get; set; }

        [JsonProperty("pool_path")] public string PoolPath { get; set; }

        [JsonProperty("progress")] public Progress Progress { get; set; }

        [JsonProperty("raidType")] public string RaidType { get; set; }

        [JsonProperty("raids")] public List<Raid> Raids { get; set; }

        [JsonProperty("scrubbingStatus")] public string ScrubbingStatus { get; set; }

        [JsonProperty("size")] public StoragePoolSize Size { get; set; }

        [JsonProperty("space_path")] public string SpacePath { get; set; }

        [JsonProperty("ssd_trim")] public SsdTrim SsdTrim { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("suggestions")] public List<object> Suggestions { get; set; }

        [JsonProperty("timebackup")] public bool Timebackup { get; set; }

        [JsonProperty("vspace_can_do")] public VspaceCanDo VspaceCanDo { get; set; }
    }
}