using Doods.Openmediavault.Rpc.Std.Data.V4;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.Std.Data.V6.Settings
{
    public class TimeSetting : V4.Settings.TimeSetting
    {
        [JsonProperty("ntpclients")] public string NtpClients { get; set; }

        public new string ToJson(bool escape)
        {
            var json = $"{{\"ntpenable\":{Ntpenable},\"ntpclients\":\"{NtpClients}\",\"ntptimeservers\":\"{Ntptimeservers}\",\"timezone\":{Timezone}}}";
            return escape ? ToEscape(json) : json;
            //return escape ? ToEscape(ToJson()) : ToJson();
        }
    }
}