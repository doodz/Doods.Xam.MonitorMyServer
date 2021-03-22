using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Webmin.Webapi.Std
{
    internal class SynologyResponse<T>
    {
        [JsonProperty("data")] public T Data { get; set; }

        [JsonProperty("success")] public bool Success { get; set; }

        [JsonProperty("error")] public SynologyError Error { get; set; }

        [JsonProperty("errors")] public List<SynologyError> Errors { get; set; }

        [JsonProperty("errormsg")] public dynamic Errormsg { get; set; }

        [JsonProperty("code")] public int Code { get; set; }

        [JsonProperty("http_status")] public int HttpStatus { get; set; }
    }
}