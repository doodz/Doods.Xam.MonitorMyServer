using System;
using System.Linq;
using Doods.Framework.Ssh.Std.Base;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// </summary>
    // <example>
    // pi@raspberrypi:~ $ nohup sudo apt-get update >/dev/null 2>&1 </dev/null &
    // [1] 1144
    // </example>
    public class NoHupQuery : GenericQuery<string>
    {
        /// <summary>
        ///     Default constructor
        /// </summary>
        /// <param name="client">the client</param>
        /// <param name="cmd">The command to execut with nohup</param>
        /// <returns>The command's pid</returns>
        public NoHupQuery(IClientSsh client, string cmd) : base(client)
        {
            CmdString = $"nohup {cmd} >/dev/null 2>&1 </dev/null & echo {ReturnQuery.ResultPid} $! &";
            //CmdString = $"nohup {cmd} >/dev/null 2>&1 &";
        }

        protected override string PaseResult(string result)
        {
            return ParsePid(result);
        }

        private string ParsePid(string result)
        {
            //var tab = result.Trim().Split(new[] {"] "}, StringSplitOptions.RemoveEmptyEntries);
            var tab = result.Trim().Split(new[] {ReturnQuery.ResultPid}, StringSplitOptions.RemoveEmptyEntries);
            if (!tab.Any()) return null;
            return tab?.Last().Trim();
        }
    }
}