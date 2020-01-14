using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class OutputBase
    {
        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("pos")]
        public long Pos { get; set; }

      

        [JsonProperty("running")]
        public bool Running { get; set; }
    }


    public class Output<T> : OutputBase
    {
      
        [JsonProperty("output")]
        public T Content { get; set; }

      
    }

    public class Output: OutputBase
    {
       

        [JsonProperty("output")]
        public string Content { get; set; }

    }
}
