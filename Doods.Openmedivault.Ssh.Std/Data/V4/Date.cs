
using System;
using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class Date
    {
        [JsonProperty("local")]
        public string Local { get; set; }

        [JsonProperty("ISO8601")]
        public DateTimeOffset Iso8601 { get; set; }
    }

    public class Plugin
    {
        [JsonProperty("abstract")]
        public string Abstract { get; set; }

        [JsonProperty("architecture")]
        public string Architecture { get; set; }

        [JsonProperty("breaks")]
        public string Breaks { get; set; }

        [JsonProperty("conflicts")]
        public string Conflicts { get; set; }

        [JsonProperty("depends")]
        public string Depends { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("descriptionmd5")]
        public string Descriptionmd5 { get; set; }

        [JsonProperty("extendeddescription")]
        public string Extendeddescription { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("homepage")]
        public Uri Homepage { get; set; }

        [JsonProperty("installed")]
        public bool Installed { get; set; }

        [JsonProperty("installedsize")]
        public long Installedsize { get; set; }

        [JsonProperty("maintainer")]
        public string Maintainer { get; set; }

        [JsonProperty("md5sum")]
        public string Md5Sum { get; set; }

        [JsonProperty("multiarch")]
        public string Multiarch { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("package")]
        public string Package { get; set; }

        [JsonProperty("pluginsection")]
        public string Pluginsection { get; set; }

        [JsonProperty("predepends")]
        public string Predepends { get; set; }

        [JsonProperty("priority")]
        public string Priority { get; set; }

        [JsonProperty("repository")]
        public string Repository { get; set; }

        [JsonProperty("section")]
        public string Section { get; set; }

        [JsonProperty("sha1")]
        public string Sha1 { get; set; }

        [JsonProperty("sha256")]
        public string Sha256 { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("suggests")]
        public string Suggests { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }
}