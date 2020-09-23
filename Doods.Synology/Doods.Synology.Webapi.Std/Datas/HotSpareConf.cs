using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public  class HotSpareConf
    {
        [JsonProperty("cross_repair")]
        public bool CrossRepair { get; set; }

        [JsonProperty("disable_repair")]
        public List<object> DisableRepair { get; set; }
    }
}