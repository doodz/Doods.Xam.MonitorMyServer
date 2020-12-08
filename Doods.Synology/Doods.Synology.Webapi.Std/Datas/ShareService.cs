using System;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public  class ShareService
    {
        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("is_service_share")]
        public bool IsServiceShare { get; set; }

        [JsonProperty("is_usb_share")]
        public bool IsUsbShare { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("uuid")]
        public Guid Uuid { get; set; }

        [JsonProperty("vol_path")]
        public string VolPath { get; set; }
    }
}