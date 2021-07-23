using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class NetstatQuery : GenericQuery<string>
    {
        public NetstatQuery(IClientSsh client) : base(client)
        {
            CmdString = "netstat -a -inet";
        }
    }
}