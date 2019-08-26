using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class PowerManagementSetting : OmvObject
    {
        [JsonProperty("cpufreq")]
        public bool Cpufreq { get; set; }

        [JsonProperty("powerbtn")]
        [JsonConverter(typeof(StringEnumConverter),converterParameters:typeof(SnakeCaseNamingStrategy))]
        public PowerbtnAction Powerbtn { get; set; }


       
    }
}