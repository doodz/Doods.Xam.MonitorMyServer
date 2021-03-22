using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyPackageInfo
    {
        [JsonProperty("package")] public List<Package> Packages { get; set; }
    }
}