using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std
{
    internal class SynologySimpleResponse<T>
    {
        [DefaultValue(null)]
       [JsonProperty("data", DefaultValueHandling = DefaultValueHandling.Populate)] public T Data { get; set; }

        [JsonProperty("success")] public bool Success { get; set; }
    }

    internal class SynologyResponse<T> : SynologySimpleResponse<T>
    {
        [JsonProperty("error",Required = Required.AllowNull)] public SynologyError Error { get; set; }

        [JsonProperty("errors",Required = Required.AllowNull)] public List<SynologyError> Errors { get; set; }

        [JsonProperty("errormsg",Required = Required.AllowNull)] public dynamic Errormsg { get; set; }

        [JsonProperty("code", Required = Required.AllowNull)] public int Code { get; set; }

        [JsonProperty("http_status", Required = Required.AllowNull)] public int HttpStatus { get; set; }
    }

    internal class SynologyResponseLoginV7<T>
    {
        [JsonProperty("error", Required = Required.AllowNull)] public SynologyErrorV7a Error { get; set; }

        [JsonProperty("success")] public bool Success { get; set; }
    }
}