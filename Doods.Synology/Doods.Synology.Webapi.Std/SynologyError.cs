using System.Collections.Generic;
using Newtonsoft.Json;

namespace Doods.Synology.Webapi.Std
{
    public class SynologyError
    {
        [JsonProperty("code", Required = Required.AllowNull)]
        public int Code { get; set; }

        [JsonProperty("path", Required = Required.AllowNull)]
        public string Path { get; set; }

        [JsonProperty("name", Required = Required.AllowNull)]
        public string Name { get; set; }

        [JsonProperty("errors", Required = Required.AllowNull)]
        public List<SynologyError> Errors { get; set; }

     

    }
    public class SynologyErrorV7a
    {
        [JsonProperty("code", Required = Required.AllowNull)]
        public int Code { get; set; }
        [JsonProperty("errors", Required = Required.AllowNull)]
        public SynologyErrorV7b Errors { get; set; }
    }
    public class SynologyErrorV7b
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("types")]
        public List<TypeElement> Types { get; set; }
    }
    public class TypeElement
    {
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}