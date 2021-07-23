using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Converters
{
    public class SshToDiskUsageConverter : SshConverter
    {
        private readonly string DF_COMMAND_HEADER_START = "Filesystem";

        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(DiskUsageBeanWhapper)) return true;

            if (objectType == typeof(IEnumerable<DiskUsageBean>)) return true;


            return objectType == typeof(DiskUsageBean);
        }

        public override object Read(string content, Type objectType)

        {
            var lines = content.Split('\n');
            var disks = new List<DiskUsageBean>();
            foreach (var line in lines)
            {
                if (line.StartsWith(DF_COMMAND_HEADER_START)) continue;

                // split string at whitespaces
                var res = line.Split().Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();

                if (res.Length >= 6)
                {
                    if (res.Length > 6)
                    {
                        // whitespace in mountpoint path
                        var sb = new StringBuilder();
                        foreach (var s in res) sb.Append(s);
                        sb.Append(" ");
                        disks.Add(new DiskUsageBean(res[0],
                            res[1], res[2],
                            res[3], res[4], sb.ToString()));
                    }
                    else
                    {
                        disks.Add(new DiskUsageBean(res[0],
                            res[1], res[2],
                            res[3], res[4],
                            res[5]));
                    }
                }
                else
                {
                    Console.WriteLine($"Expected another output of df -h. Skipping line: {line}");
                }
            }

            if (objectType == typeof(DiskUsageBeanWhapper)) return new DiskUsageBeanWhapper(disks);
            return disks;
        }
    }
}