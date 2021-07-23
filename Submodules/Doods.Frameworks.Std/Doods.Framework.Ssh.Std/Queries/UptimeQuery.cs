using System;
using System.Globalization;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     cat /proc/uptime
    ///     1106378.63 4430593.31
    /// </example>
    public class UptimeQuery : GenericQuery<double>
    {
        private readonly string UPTIME_CMD = "cat /proc/uptime";

        public UptimeQuery(IClientSsh client) : base(client)
        {
            CmdString = UPTIME_CMD;
        }

        protected override double PaseResult(string result)
        {
            return FormatUptime(result);
        }

        private double FormatUptime(string output)
        {
            var lines = output.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var split = line.Split(' ');
                if (split.Length == 2)
                    try
                    {
                        return double.Parse(split[0], CultureInfo.InvariantCulture);
                    }
                    catch (FormatException)
                    {
                        Client.Logger.Debug($"Skipping line: {line}");
                    }
                else
                    Client.Logger.Debug($"Skipping line: {line}");
            }

            Client.Logger.Error($"Expected a different output of command: {UPTIME_CMD}");
            Client.Logger.Error($"Actual output was: {output}");
            return 0D;
        }
    }
}