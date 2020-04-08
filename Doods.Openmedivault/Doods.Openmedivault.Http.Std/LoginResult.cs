using Newtonsoft.Json;

namespace Doods.Openmedivault.Http.Std
{
    public class LoginResult
    {
        [JsonProperty("authenticated")]
        public bool Authenticated { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }
}