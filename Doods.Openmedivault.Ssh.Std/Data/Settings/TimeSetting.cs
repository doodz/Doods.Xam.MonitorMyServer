using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class TimeSetting : OmvObject
    {
        [JsonProperty("date")]
        public Date Date { get; set; }

        [JsonProperty("timezone")]
        public string Timezone { get; set; }

        [JsonProperty("ntpenable")]
        public bool Ntpenable { get; set; }

        [JsonProperty("ntptimeservers")]
        public string Ntptimeservers { get; set; }


        public new string ToJson(bool escape)
        {
            var json = $"{{\"ntpenable\":{Ntpenable},\"ntptimeservers\":\"{Ntptimeservers}\",\"timezone\":{Timezone}}}";
            return escape ? ToEscape(json) : json;
            //return escape ? ToEscape(ToJson()) : ToJson();

        }
    }
}