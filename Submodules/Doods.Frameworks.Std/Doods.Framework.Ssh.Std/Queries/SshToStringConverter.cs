using System;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class SshToStringConverter : SshConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object Read(string reader, Type objectType)
        {
            return reader;
        }
    }
}