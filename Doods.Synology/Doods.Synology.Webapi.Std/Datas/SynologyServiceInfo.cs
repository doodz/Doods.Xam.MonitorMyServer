using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynologyServiceInfo
    {
        [JsonProperty("service")]
        public List<Service> Services { get; set; }
      
    }
}