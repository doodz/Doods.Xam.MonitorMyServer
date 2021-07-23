using System;
using System.Collections.Generic;
using System.Linq;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Requests
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     groups
    ///     pi adm dialout cdrom sudo audio video plugdev games users input netdev gpio i2c spi
    /// </example>
    public class GroupsRequest : SshRequestBase
    {
        public const string RequestString = "groups";

        public GroupsRequest() : base(RequestString)
        {
            _SshSerializer = new SshSerializer(new SshSerializerSettings
                {Converters = new List<ISshConverter> {new GroupsRequestConverter()}});
        }

        private class GroupsRequestConverter : ISshConverter
        {
            public bool CanConvert(Type objectType)
            {
                if (objectType == typeof(IEnumerable<string>)) return true;
                if (objectType == typeof(string[])) return true;
                return false;
            }

            public object Read(string reader, Type objectType)
            {
                var result = Getlist(reader);
                if (objectType == typeof(IEnumerable<string>)) return result.ToList();

                return result;
            }


            private string[] Getlist(string output)
            {
                var lines = output.Split(new[] {"\r\n", "\n", " "}, StringSplitOptions.RemoveEmptyEntries);
                return lines;
            }
        }
    }
}