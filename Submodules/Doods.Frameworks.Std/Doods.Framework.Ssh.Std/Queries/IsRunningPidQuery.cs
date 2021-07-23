using System;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     pi @raspberrypi:~ $ ps -p 42
    ///     PID TTY          TIME CMD
    ///     42 ?        00:00:00 nfsiod
    ///     pi@raspberrypi:~ $ ps -p 1052334
    ///     PID TTY          TIME CMD
    /// </example>
    public class IsRunningPidQuery : GenericQuery<bool>
    {
        public IsRunningPidQuery(IClientSsh client, string pid) : base(client)
        {
            CmdString = $"ps -p {pid}";
        }

        protected override bool PaseResult(string result)
        {
            var lines = result.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
            if (lines.Length < 2)
            {
                Client.Logger.Error("Could not parse processes.");
                return false;
            }

            return true;
        }
    }
}