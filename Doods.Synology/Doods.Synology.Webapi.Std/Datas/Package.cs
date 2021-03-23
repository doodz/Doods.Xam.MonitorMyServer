using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Package
    {
        [JsonProperty("additional")] public PackageAdditional Additional { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("version")] public string Version { get; set; }
    }

    public class PackageAdditional
    {
        //[JsonProperty("dependent_packages")]
        //public DependentPackages DependentPackages { get; set; }

        [JsonProperty("start_dependent_services")]
        public List<string> StartDependentServices { get; set; }

        [JsonProperty("startable")] public bool Startable { get; set; }

        [JsonProperty("status")] public PurpleStatus Status { get; set; }
    }
    //public partial class DependentPackages
    //{
    //    [JsonProperty("SynologyDrive", NullValueHandling = NullValueHandling.Ignore)]
    //    public string SynologyDrive { get; set; }

    //    //[JsonProperty("SynoFinder", NullValueHandling = NullValueHandling.Ignore)]
    //    //public Php70 SynoFinder { get; set; }

    //    //[JsonProperty("SynologyApplicationService", NullValueHandling = NullValueHandling.Ignore)]
    //    //public Php70 SynologyApplicationService { get; set; }

    //    [JsonProperty("ReplicationService", NullValueHandling = NullValueHandling.Ignore)]
    //    public string ReplicationService { get; set; }

    //    [JsonProperty("Node.js_v12", NullValueHandling = NullValueHandling.Ignore)]
    //    public string NodeJsV12 { get; set; }

    //    [JsonProperty("PHP7.0", NullValueHandling = NullValueHandling.Ignore)]
    //    public Php70 Php70 { get; set; }

    //    [JsonProperty("ffmpeg", NullValueHandling = NullValueHandling.Ignore)]
    //    public Ffmpeg Ffmpeg { get; set; }
    //}
}