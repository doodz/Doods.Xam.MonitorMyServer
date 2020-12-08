using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyStorageInfo
    {
        [JsonProperty("disks")]
        public List<Disk> Disks { get; set; }

        [JsonProperty("env")]
        public Env Env { get; set; }

        [JsonProperty("hotSpareConf")]
        public HotSpareConf HotSpareConf { get; set; }

        [JsonProperty("hotSpares")]
        public List<object> HotSpares { get; set; }

        [JsonProperty("iscsiLuns")]
        public List<object> IscsiLuns { get; set; }

        [JsonProperty("iscsiTargets")]
        public List<object> IscsiTargets { get; set; }

        [JsonProperty("ports")]
        public List<object> Ports { get; set; }

        [JsonProperty("ssdCaches")]
        public List<object> SsdCaches { get; set; }

        [JsonProperty("storagePools")]
        public List<StoragePool> StoragePools { get; set; }

        [JsonProperty("volumes")]
        public List<Volume> Volumes { get; set; }
    }
}