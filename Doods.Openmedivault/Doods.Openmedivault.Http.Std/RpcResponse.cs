using Newtonsoft.Json;

namespace Doods.Openmedivault.Http.Std
{
    public class RpcResponse<T>
    {
        [JsonProperty("response", Required = Required.AllowNull)]
        public T Response { get; set; }

        [JsonProperty("error", Required = Required.AllowNull)]
        public object Error { get; set; }
    }
}