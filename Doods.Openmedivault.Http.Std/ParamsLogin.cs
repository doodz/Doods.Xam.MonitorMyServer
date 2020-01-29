using Newtonsoft.Json;

namespace Doods.Openmedivault.Http.Std
{
    public class ParamsLogin
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}