using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     kill 32251
    ///     [1]+  Terminated              bash -c 'exec -a sadhadxk sleep 1000000'
    ///     kill 32251
    ///     -bash: kill: (32251) - No such process
    ///     kill 32251
    ///     -bash: kill: (32336) - Operation not permitted
    /// </example>
    public class KillProcessQuery : GenericQuery<bool>
    {
        public KillProcessQuery(IClientSsh client, int pid) : base(client)
        {
            CmdString = $"kill {pid}";
        }

        protected override bool PaseResult(string result)
        {
            return ParseProcesses(result);
        }

        /// <summary>
        ///     Parses the output of the ps command.
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        private bool ParseProcesses(string output)
        {
            if (output.Contains("kill:"))
                return false;
            return true;
        }
    }
}