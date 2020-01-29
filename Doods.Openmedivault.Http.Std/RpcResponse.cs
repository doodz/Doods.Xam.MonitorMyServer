using Newtonsoft.Json;

namespace Doods.Openmedivault.Http.Std
{
    public class RpcResponse<T>
    {
        [JsonProperty("response")] public T Response { get; set; }

        [JsonProperty("error")] public string Error { get; set; }
    }
}