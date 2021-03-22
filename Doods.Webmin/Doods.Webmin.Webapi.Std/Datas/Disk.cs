using System.Collections.Generic;
using Doods.Webmin.Webapi.Std.Classes;
using Newtonsoft.Json;

namespace Doods.Webmin.Webapi.Std.Datas
{
    public class Disk
    {
        [JsonProperty("y")]
        [JsonConverter(typeof(DecodeArrayConverter))]
        public List<long> Y { get; set; }

        [JsonProperty("x")] public long X { get; set; }
    }
}