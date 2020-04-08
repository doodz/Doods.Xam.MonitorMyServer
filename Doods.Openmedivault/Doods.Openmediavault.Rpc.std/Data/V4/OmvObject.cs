using Doods.Openmedivault.Ssh.Std.Data;
using Newtonsoft.Json;

namespace Doods.Openmediavault.Rpc.std.Data.V4
{
    public abstract class OmvObject:IOmvObject
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }


        public string ToJson(bool escape)
        {
            return escape ? ToEscape(ToJson()) : ToJson();

        }
        internal string ToEscape(string value)
        {
            return value.Replace("\"", "\\\"");
        }

    }
}