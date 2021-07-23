using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Converters
{
    public class SshToSimpleStringConverter : SshConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(IEnumerable<string>)) return true;
            else if (objectType.GetInterfaces().Any(i => i == typeof(IEnumerable<string>))) return true;
            return false;
        }

        public override object Read(string content, Type objectType)
        {
            var lines = content.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (!objectType.IsArray)
            {
                if (objectType == typeof(Collection<string>))
                {
                    return new Collection<string>(lines);
                }

                return lines.ToList();


            }

            return lines;
        }
    }
}