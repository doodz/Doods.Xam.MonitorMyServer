using System;
using System.Text;
using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
{
    public class SystemInformation : IOmvObject
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public ValueUnion Value { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("index")]
        public long Index { get; set; }
    }

    public  class ValueClass
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }
    }

    public  struct ValueUnion
    {
        public string ValueUnionString;
        public ValueClass ValueClass;

        public static implicit operator ValueUnion(string String) => new ValueUnion { ValueUnionString = String };
        public static implicit operator ValueUnion(ValueClass ValueClass) => new ValueUnion { ValueClass = ValueClass };


        public string SimpleStringValue
        {
            get { return ToString(); }
        }

        public override string ToString()
        {
            if (ValueUnionString != null) return ValueUnionString;

            return ValueClass?.Text;
        }
    }

  

}
