using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class IsRunning
    {

            [JsonProperty("filename")]
            public string Filename { get; set; }

            [JsonProperty("running")]
            public bool Running { get; set; }
        
    }
}
