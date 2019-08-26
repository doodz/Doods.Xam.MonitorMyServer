using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data.FileSystem
{
    public class CandidateFileSystem : IOmvObject
    {
        [JsonProperty("devicefile")]
        public string Devicefile { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

   }
