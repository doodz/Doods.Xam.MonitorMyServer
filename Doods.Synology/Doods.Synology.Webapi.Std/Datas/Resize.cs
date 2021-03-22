using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std.Datas
{
    public class Resize
    {
        [JsonProperty("can_do")] public bool CanDo { get; set; }

        [JsonProperty("errCode")] public long ErrCode { get; set; }

        [JsonProperty("stopService")] public bool StopService { get; set; }
    }
}