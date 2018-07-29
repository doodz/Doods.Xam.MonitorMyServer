using System;
using System.Linq;
using System.Threading.Tasks;
using Doods.Framework.Ssh.Std.Base;
using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class NuHupQueryWithWaitPid : GenericQuery<bool>
    {
        public NuHupQueryWithWaitPid(IClientSsh client, string cmd) : base(client)
        {
            CmdString = $"nohup {cmd} >/dev/null 2>&1 </dev/null & echo {ReturnQuery.ResultPid} $! &";
        }

        protected override bool PaseResult(string result)
        {
            var str = ParsePid(result);

            if (string.IsNullOrWhiteSpace(str)) return false;

            bool isRunningIpd;
            var delay = 1000;
            do
            {
                Task.Delay(delay).Wait();
                
                isRunningIpd = new IsRunningPidQuery(Client, str).Run();
                delay += 1000;
                if (delay > 5000)
                    delay = 1000;
            } while (isRunningIpd);

            return true;
        }

        private string ParsePid(string result)
        {
            var tab = result.Trim().Split(new[] {ReturnQuery.ResultPid}, StringSplitOptions.RemoveEmptyEntries);
            if (!tab.Any()) return null;
            return tab?.Last().Trim();
        }
    }
}