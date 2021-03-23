using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Container
    {
        [JsonProperty("order")] public long Order { get; set; }

        [JsonProperty("str")] public string Str { get; set; }

        [JsonProperty("supportPwrBtnDisable")] public bool SupportPwrBtnDisable { get; set; }

        [JsonProperty("type")] public string Type { get; set; }
    }
}