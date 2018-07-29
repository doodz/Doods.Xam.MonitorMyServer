using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class InstallQuery : GenericQuery<bool>
    {
        public InstallQuery(IClientSsh client, string packageName) : base(client)
        {
            CmdString = $"sudo apt-get install {packageName}";
        }
    }
}
