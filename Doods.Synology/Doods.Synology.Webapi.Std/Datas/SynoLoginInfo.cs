using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.NewFolder
{
    public class SynoLoginResult
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("ik_message")]
        public string IkMessage { get; set; }

        [JsonProperty("is_portal_port")]
        public bool IsPortalPort { get; set; }

        [JsonProperty("sid")]
        public string Sid { get; set; }

        [JsonProperty("synotoken")]
        public string Synotoken { get; set; }
    }
    public class SynoAuthType
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
    public class SynoLoginInfo
    {
        [JsonProperty("sid")] public string Sid { get; set; }
    }

    public enum RequestFormat
    {
        Json
    }
}