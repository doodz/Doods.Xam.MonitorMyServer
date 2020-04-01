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
}