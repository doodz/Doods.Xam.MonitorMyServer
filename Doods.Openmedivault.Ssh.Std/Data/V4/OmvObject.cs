﻿using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Data
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