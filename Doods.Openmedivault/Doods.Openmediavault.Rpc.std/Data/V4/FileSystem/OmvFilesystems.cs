using System.Collections.Generic;
using Doods.Openmedivault.Ssh.Std.Data;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.std.Data.V4.FileSystem
{
    public class OmvFilesystems : BaseOmvFilesystems
    {
        [JsonProperty("parentdevicefile")] public string Parentdevicefile { get; set; }
        [JsonProperty("uuid")] public string Uuid { get; set; }

        [JsonProperty("blocks")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Blocks { get; set; }

        [JsonProperty("mounted")] public bool Mounted { get; set; }

        [JsonProperty("mountpoint")] public string Mountpoint { get; set; }

        [JsonProperty("used")] public string ResponseUsed { get; set; }

        [JsonProperty("available")] public string Available { get; set; }

        [JsonProperty("size")] public string Size { get; set; }

        [JsonProperty("percentage")] public long Percentage { get; set; }

        [JsonProperty("description")] public string Description { get; set; }

        [JsonProperty("propposixacl")] public bool Propposixacl { get; set; }

        [JsonProperty("propquota")] public bool Propquota { get; set; }

        [JsonProperty("propresize")] public bool Propresize { get; set; }

        [JsonProperty("propfstab")] public bool Propfstab { get; set; }

        [JsonProperty("propreadonly")] public bool Propreadonly { get; set; }

        [JsonProperty("propcompress")] public bool Propcompress { get; set; }

        [JsonProperty("propautodefrag")] public bool Propautodefrag { get; set; }

        [JsonProperty("hasmultipledevices")] public bool Hasmultipledevices { get; set; }

        [JsonProperty("devicefiles")] public List<string> Devicefiles { get; set; }

        [JsonProperty("_readonly")] public bool Readonly { get; set; }

        [JsonProperty("_used")] public bool Used { get; set; }

        [JsonIgnore] public bool CanMount => !Mounted && !Readonly && Propfstab;

        [JsonIgnore] public bool CanUmount => Mounted && !Readonly && Propfstab;

        [JsonIgnore] public bool CanDelete => !Used && !Readonly && Propfstab;
    }
}