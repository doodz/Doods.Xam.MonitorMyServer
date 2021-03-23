using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class SynolgySharesInfo
    {
        [JsonProperty("shares")] public List<Share> Shares { get; set; }

        [JsonProperty("total")] public long Total { get; set; }
    }
}