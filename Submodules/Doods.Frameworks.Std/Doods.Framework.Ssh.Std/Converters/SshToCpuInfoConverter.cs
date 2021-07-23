using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Doods.Framework.Ssh.Std.Beans;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Converters
{
    public class SshToCpuInfoConverter : SshConverter
    {
        private readonly Regex _pattern = new Regex(@"^(.*):\s*(.*)");

        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>
        ///     <c>true</c> if this instance can convert the specified object type; otherwise, <c>false</c>.
        /// </returns>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(CpuInfoBean);
        }

        public override object Read(string content, Type objectType)
        {
            var lines = content.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);

            var res = new CpuInfoBean();

            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(':');
                var key = line[0].Trim();
                var value = line[1].Trim();
                switch (key)
                {
                    case "Architecture":
                        res.Architecture = value;
                        break;
                    case "Byte Order":
                        res.ByteOrder = value;
                        break;
                    case "CPU(s)":
                        res.Cpu = int.Parse(value);
                        break;
                    case "On - line CPU(s) list":
                        res.OnlineCpuList = value;
                        break;
                    case "Thread(s) per core":
                        res.ThreadPerCore = int.Parse(value);
                        break;
                    case "Core(s) per socket":
                        res.CorePerSocket = int.Parse(value);
                        break;
                    case "Socket(s)":
                        res.Socket = int.Parse(value);
                        break;
                    case "Model name":
                        res.ModelName = value;
                        break;
                    case "CPU max MHz":
                        res.CpuMaxMHz = double.Parse(value, CultureInfo.InvariantCulture);
                        break;
                    case "CPU min MHz":
                        res.CpuMinMHz = double.Parse(value, CultureInfo.InvariantCulture);
                        break;
                    case "CPU op-mode(s)":
                        res.CpuOpMode = value;
                        break;
                    case "Vendor ID:":
                        res.VendorId = value;
                        break;
                }
            }

            return res;
        }
    }
}