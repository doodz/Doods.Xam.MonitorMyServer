using System;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     cat /proc/cpuinfo | grep Serial
    ///     Serial          : 00000000ada830b4
    /// </example>
    public class SerialNoQuery : GenericQuery<string>
    {
        private readonly string CAT_PROC_CPUINFO_GREP_SERIAL = "cat /proc/cpuinfo | grep Serial";
        private readonly string N_A = "n/a";

        public SerialNoQuery(IClientSsh client) : base(client)
        {
            CmdString = CAT_PROC_CPUINFO_GREP_SERIAL;
        }

        protected override string PaseResult(string result)
        {
            return FormatCpuSerial(result);
        }

        private string FormatCpuSerial(string output)
        {
            var split = output.Trim().Split(':');
            if (split.Length >= 2)
            {
                var cpuSerial = split[1].Trim();
                return cpuSerial;
            }

            Client.Logger.Error(
                $"Could not query cpu serial number. Expected another output of '{CAT_PROC_CPUINFO_GREP_SERIAL}'.");
            Client.Logger.Error($"Output of '{CAT_PROC_CPUINFO_GREP_SERIAL}': {Environment.NewLine}{output}");
            return N_A;
        }
    }
}