using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class MountQuery : GenericQuery<bool>
    {
        public MountQuery(IClientSsh client, string device) : base(client)
        {
            CmdString = $"mount {device}";
        }

        protected override bool PaseResult(string result)
        {
            return !result.Contains("mount");
        }
    }
}
