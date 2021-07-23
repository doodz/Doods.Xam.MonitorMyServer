using System.Collections.Generic;
using Doods.Framework.Std;
using Newtonsoft.Json;

namespace Doods.Framework.Ssh.Std.Beans
{
    public class BlockdeviceWrapper
    {
        [JsonProperty("blockdevices")] public List<Blockdevice> BlockdevicesBlockdevices { get; set; }
    }

    public class Blockdevice : NotifyPropertyChangedBase
    {
        [JsonIgnore] public static string TypeDisk = "Disk";

        [JsonIgnore] public static string TypeRaid = "raid";

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("kname")] public string Kname { get; set; }

        [JsonProperty("path")] public string Path { get; set; }

        [JsonProperty("maj:min")] public string MajMin { get; set; }

        [JsonProperty("fsavail")] public string Fsavail { get; set; }

        [JsonProperty("fssize")] public string Fssize { get; set; }

        [JsonProperty("fstype")] public string Fstype { get; set; }

        [JsonProperty("fsused")] public string Fsused { get; set; }

        [JsonProperty("fsuse%")] public string Fsuse { get; set; }

        [JsonProperty("mountpoint")] public string Mountpoint { get; set; }

        [JsonProperty("label")] public string Label { get; set; }

        [JsonProperty("uuid")] public string Uuid { get; set; }

        [JsonProperty("ptuuid")] public string Ptuuid { get; set; }

        [JsonProperty("pttype")] public string Pttype { get; set; }

        [JsonProperty("parttype")] public string Parttype { get; set; }

        [JsonProperty("partlabel")] public object Partlabel { get; set; }

        [JsonProperty("partuuid")] public string Partuuid { get; set; }

        [JsonProperty("partflags")] public string Partflags { get; set; }

        [JsonProperty("ra")] public long Ra { get; set; }

        [JsonProperty("ro")] public bool Ro { get; set; }

        [JsonProperty("rm")] public bool Rm { get; set; }

        [JsonProperty("hotplug")] public bool Hotplug { get; set; }

        [JsonProperty("model")] public string Model { get; set; }

        [JsonProperty("serial")] public string Serial { get; set; }

        [JsonProperty("size")] public long Size { get; set; }

        [JsonProperty("state")] public string State { get; set; }

        [JsonProperty("owner")] public object Owner { get; set; }

        [JsonProperty("group")] public object Group { get; set; }

        [JsonProperty("mode")] public string Mode { get; set; }

        [JsonProperty("alignment")] public long Alignment { get; set; }

        [JsonProperty("min-io")] public long MinIo { get; set; }

        [JsonProperty("opt-io")] public long OptIo { get; set; }

        [JsonProperty("phy-sec")] public long PhySec { get; set; }

        [JsonProperty("log-sec")] public long LogSec { get; set; }

        [JsonProperty("rota")] public bool Rota { get; set; }

        [JsonProperty("sched")] public string Sched { get; set; }

        [JsonProperty("rq-size")] public long RqSize { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("disc-aln")] public long DiscAln { get; set; }

        [JsonProperty("disc-gran")] public long DiscGran { get; set; }

        [JsonProperty("disc-max")] public long DiscMax { get; set; }

        [JsonProperty("disc-zero")] public bool DiscZero { get; set; }

        [JsonProperty("wsame")] public long Wsame { get; set; }

        [JsonProperty("wwn")] public string Wwn { get; set; }

        [JsonProperty("rand")] public bool Rand { get; set; }

        [JsonProperty("pkname")] public string Pkname { get; set; }

        [JsonProperty("hctl")] public string Hctl { get; set; }

        [JsonProperty("tran")] public string Tran { get; set; }

        [JsonProperty("subsystems")] public string Subsystems { get; set; }

        [JsonProperty("rev")] public string Rev { get; set; }

        [JsonProperty("vendor")] public string Vendor { get; set; }

        [JsonProperty("zoned")] public string Zoned { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public List<Blockdevice> Children { get; set; }
    }
}