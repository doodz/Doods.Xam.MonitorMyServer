using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyPackageInfo
    {
        [JsonProperty("packages")] public List<Package> Packages { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class SynologyPackageServerInfo
    {
        [JsonProperty("banners")]
        public List<Banner> Banners { get; set; }

        [JsonProperty("beta_packages")]
        public List<BetaPackage> BetaPackages { get; set; }

        [JsonProperty("blupgrade")]
        public bool Blupgrade { get; set; }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        [JsonProperty("packages")]
        public List<Package> Packages { get; set; }
    }





    public class BetaPackage
    {
        [JsonProperty("beta")]
        public bool Beta { get; set; }

        [JsonProperty("blupgrade")]
        public bool Blupgrade { get; set; }

        [JsonProperty("breakpkgs")]
        public string Breakpkgs { get; set; }

        [JsonProperty("category")]
        public List<Categorys> Category { get; set; }

        [JsonProperty("changelog")]
        public string Changelog { get; set; }

        [JsonProperty("conflictpkgs")]
        public string Conflictpkgs { get; set; }

        [JsonProperty("deppkgs")]
        public string Deppkgs { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("distributor_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri DistributorUrl { get; set; }

        [JsonProperty("dname")]
        public string Dname { get; set; }

        [JsonProperty("download_count")]
        public long DownloadCount { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("is_security_version")]
        public bool IsSecurityVersion { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }

        [JsonProperty("maintainer")]
        public string Maintainer { get; set; }

        [JsonProperty("maintainer_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri MaintainerUrl { get; set; }

        [JsonProperty("md5")]
        public string Md5 { get; set; }

        [JsonProperty("package")]
        public string Package { get; set; }

        [JsonProperty("price")]
        public object Price { get; set; }

        [JsonProperty("qinst", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Qinst { get; set; }

        [JsonProperty("qstart")]
        public bool Qstart { get; set; }

        [JsonProperty("qupgrade", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Qupgrade { get; set; }

        [JsonProperty("recent_download_count")]
        public long RecentDownloadCount { get; set; }

        [JsonProperty("replace_message", NullValueHandling = NullValueHandling.Ignore)]
        public string ReplaceMessage { get; set; }

        [JsonProperty("replaceforcepkgs")]
        public object Replaceforcepkgs { get; set; }

        [JsonProperty("replacepkgs")]
        public BetaPackageReplacepkgs Replacepkgs { get; set; }

        [JsonProperty("silent_install")]
        public bool SilentInstall { get; set; }

        [JsonProperty("silent_uninstall", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SilentUninstall { get; set; }

        [JsonProperty("silent_upgrade")]
        public bool SilentUpgrade { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("snapshot")]
        public List<Uri> Snapshot { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("start")]
        public bool Start { get; set; }

        [JsonProperty("thumbnail")]
        public List<string> Thumbnail { get; set; }

        [JsonProperty("thumbnail_retina")]
        public List<string> ThumbnailRetina { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("auto_upgrade_from", NullValueHandling = NullValueHandling.Ignore)]
        public string AutoUpgradeFrom { get; set; }

        [JsonProperty("depsers", NullValueHandling = NullValueHandling.Ignore)]
        public string Depsers { get; set; }
    }
    public class BetaPackageReplacepkgs
    {
        [JsonProperty("CloudStation")]
        public string CloudStation { get; set; }

        [JsonProperty("CloudStationClient")]
        public string CloudStationClient { get; set; }
    }
    public enum Categorys { Backup, Business, Devtools, ManagementTools, Multimedia, Productivity, Security, Utilities };
}