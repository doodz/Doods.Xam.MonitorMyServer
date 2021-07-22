using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyPackageServer
    {
        [JsonProperty("banners")]
        public List<Banner> Banners { get; set; }

        [JsonProperty("beta_packages")]
        public List<PackageServer> BetaPackages { get; set; }

        [JsonProperty("blupgrade")]
        public bool Blupgrade { get; set; }

        [JsonProperty("categories")]
        public List<Category> Categories { get; set; }

        [JsonProperty("packages")]
        public List<PackageServer> Packages { get; set; }
    }
    public  class Category
    {
        [JsonProperty("descr")]
        public string Descr { get; set; }

        [JsonProperty("dname")]
        public string Dname { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isCompilation")]
        public bool IsCompilation { get; set; }
    }
    public  class Banner
    {
        [JsonProperty("background")]
        public Uri Background { get; set; }

        [JsonProperty("beta")]
        public bool Beta { get; set; }

        [JsonProperty("css")]
        public Css Css { get; set; }

        [JsonProperty("descr")]
        public string Descr { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("keyword")]
        public string Keyword { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("sort")]
        public long Sort { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public  class Css
    {
        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("string")]
        public string String { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("triangle")]
        public string Triangle { get; set; }
    }

    public  class PackageServer 
    {
        [JsonProperty("beta", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Beta { get; set; }
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("blupgrade")]
        public bool Blupgrade { get; set; }

        [JsonProperty("breakpkgs")]
        public Breakpkgs Breakpkgs { get; set; }

        [JsonProperty("category")]
        public List<string> Category { get; set; }

        [JsonProperty("changelog")]
        public string Changelog { get; set; }

        [JsonProperty("conflictpkgs")]
        public object Conflictpkgs { get; set; }

        [JsonProperty("deppkgs")]
        public object Deppkgs { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("distributor_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri DistributorUrl { get; set; }

        [JsonProperty("dname")]
        public string Dname { get; set; }

        [JsonProperty("download_count")]
        public long DownloadCount { get; set; }

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
        public string PackagePackage { get; set; }

        [JsonProperty("price")]
        public object Price { get; set; }

        [JsonProperty("qinst", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Qinst { get; set; }

        [JsonProperty("qstart", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Qstart { get; set; }

        [JsonProperty("qupgrade", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Qupgrade { get; set; }

        [JsonProperty("recent_download_count")]
        public long RecentDownloadCount { get; set; }

        [JsonProperty("replace_message", NullValueHandling = NullValueHandling.Ignore)]
        public string ReplaceMessage { get; set; }

        [JsonProperty("replaceforcepkgs")]
        public Replaceforcepkgs Replaceforcepkgs { get; set; }

        [JsonProperty("replacepkgs")]
        public Replacepkgs Replacepkgs { get; set; }

        [JsonProperty("silent_install", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SilentInstall { get; set; }

        [JsonProperty("silent_uninstall", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SilentUninstall { get; set; }

        [JsonProperty("silent_upgrade", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SilentUpgrade { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("snapshot")]
        public List<Uri> Snapshot { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("start", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Start { get; set; }

        [JsonProperty("thumbnail")]
        public List<string> Thumbnail { get; set; }

        [JsonProperty("thumbnail_retina")]
        public List<string> ThumbnailRetina { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("ctl_stop", NullValueHandling = NullValueHandling.Ignore)]
        public string CtlStop { get; set; }

        [JsonProperty("distributor", NullValueHandling = NullValueHandling.Ignore)]
        public string Distributor { get; set; }

        [JsonProperty("thirdparty", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Thirdparty { get; set; }

        [JsonProperty("support_conf_folder", NullValueHandling = NullValueHandling.Ignore)]
        public bool? SupportConfFolder { get; set; }

        [JsonProperty("support_url", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SupportUrl { get; set; }

        [JsonProperty("depsers", NullValueHandling = NullValueHandling.Ignore)]
        public string Depsers { get; set; }

        [JsonProperty("install_on_cold_storage", NullValueHandling = NullValueHandling.Ignore)]
        public bool? InstallOnColdStorage { get; set; }

        [JsonProperty("ctl_uninstall", NullValueHandling = NullValueHandling.Ignore)]
        public string CtlUninstall { get; set; }

        [JsonProperty("install_type", NullValueHandling = NullValueHandling.Ignore)]
        public string InstallType { get; set; }

        [JsonProperty("auto_upgrade_from", NullValueHandling = NullValueHandling.Ignore)]
        public string AutoUpgradeFrom { get; set; }

        [JsonProperty("startable", NullValueHandling = NullValueHandling.Ignore)]
        public string Startable { get; set; }

        [JsonProperty("model", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Model { get; set; }
    }

    public partial class Breakpkgs
    {
        [JsonProperty("Docker-Discourse", NullValueHandling = NullValueHandling.Ignore)]
        public object DockerDiscourse { get; set; }

        [JsonProperty("Docker-GitLab", NullValueHandling = NullValueHandling.Ignore)]
        public object DockerGitLab { get; set; }

        [JsonProperty("Docker-LXQt", NullValueHandling = NullValueHandling.Ignore)]
        public object DockerLxQt { get; set; }

        [JsonProperty("Docker-Redmine", NullValueHandling = NullValueHandling.Ignore)]
        public object DockerRedmine { get; set; }

        [JsonProperty("Docker-Spree", NullValueHandling = NullValueHandling.Ignore)]
        public object DockerSpree { get; set; }

        [JsonProperty("DocumentViewer", NullValueHandling = NullValueHandling.Ignore)]
        public object DocumentViewer { get; set; }

        [JsonProperty("MailClient", NullValueHandling = NullValueHandling.Ignore)]
        public object MailClient { get; set; }

        [JsonProperty("PACS", NullValueHandling = NullValueHandling.Ignore)]
        public object Pacs { get; set; }

        [JsonProperty("Contacts", NullValueHandling = NullValueHandling.Ignore)]
        public object Contacts { get; set; }

        [JsonProperty("Spreadsheet", NullValueHandling = NullValueHandling.Ignore)]
        public object Spreadsheet { get; set; }

        [JsonProperty("SnapshotReplication", NullValueHandling = NullValueHandling.Ignore)]
        public object SnapshotReplication { get; set; }

        [JsonProperty("PhotoStation", NullValueHandling = NullValueHandling.Ignore)]
        public object PhotoStation { get; set; }
    }
    public  class Replacepkgs
    {
        [JsonProperty("ActiveDirectoryServer", NullValueHandling = NullValueHandling.Ignore)]
        public string ActiveDirectoryServer { get; set; }

        [JsonProperty("CloudStation", NullValueHandling = NullValueHandling.Ignore)]
        public string CloudStation { get; set; }

        [JsonProperty("CloudStationClient", NullValueHandling = NullValueHandling.Ignore)]
        public string CloudStationClient { get; set; }
    }

    public class Replaceforcepkgs
    {
        [JsonProperty("ActiveDirectoryServer")]
        public string ActiveDirectoryServer { get; set; }
    }
}