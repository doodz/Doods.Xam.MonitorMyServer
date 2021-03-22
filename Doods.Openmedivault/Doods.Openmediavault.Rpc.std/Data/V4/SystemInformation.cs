using Doods.Openmedivault.Ssh.Std.Data;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.std.Data.V4
{
    public class SystemInformation : IOmvObject
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("value")] public ValueUnion Value { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("index")] public long Index { get; set; }
    }

    public class ValueClass
    {
        [JsonProperty("text")] public string Text { get; set; }

        [JsonProperty("value")] public double Value { get; set; }
    }

    public struct ValueUnion
    {
        public string ValueUnionString;
        public ValueClass ValueClass;

        public static implicit operator ValueUnion(string String)
        {
            return new ValueUnion {ValueUnionString = String};
        }

        public static implicit operator ValueUnion(ValueClass ValueClass)
        {
            return new ValueUnion {ValueClass = ValueClass};
        }


        public string SimpleStringValue => ToString();

        public override string ToString()
        {
            if (ValueUnionString != null) return ValueUnionString;

            return ValueClass?.Text;
        }
    }
}