using System;
using System.Collections.Generic;
using System.Globalization;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Framework.Ssh.Std.Requests
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     cat /proc/uptime
    ///     1106378.63 4430593.31
    /// </example>
    public class UptimeRequest : SshRequestBase
    {
        public const string RequestString = "cat /proc/uptime";

        public UptimeRequest() : base(RequestString)
        {
            _SshSerializer = new SshSerializer(new SshSerializerSettings
                {Converters = new List<ISshConverter> {new UptimeRequestConverter()}});
        }

        private class UptimeRequestConverter : ISshConverter
        {
            public bool CanConvert(Type objectType)
            {
                if (objectType == typeof(string)) return true;
                if (objectType == typeof(double)) return true;
                if (objectType == typeof(TimeSpan)) return true;
                if (objectType == typeof(DateTime)) return true;

                return false;
            }

            public object Read(string reader, Type objectType)
            {
                var result = FormatUptime(reader);
                if (objectType == typeof(double)) return result;

                var timeSpan = TimeSpan.FromSeconds(result);
                if (objectType == typeof(TimeSpan))
                    return timeSpan;

                return timeSpan.ToString(string.Empty);
            }


            private double FormatUptime(string output)
            {
                var lines = output.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var split = line.Split(' ');
                    if (split.Length == 2)
                    {
                        try
                        {
                            return double.Parse(split[0], CultureInfo.InvariantCulture);
                        }
                        catch (FormatException)
                        {
                        }
                    }
                }

                return 0D;
            }
        }
    }
}