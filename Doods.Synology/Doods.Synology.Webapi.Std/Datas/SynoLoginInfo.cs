using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class SynoLoginInfo
    {
        [JsonProperty("sid")] public string Sid { get; set; }
    }

    public enum RequestFormat
    {
        Json
    }
}