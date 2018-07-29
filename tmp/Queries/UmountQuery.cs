using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class UmountQuery : GenericQuery<bool>
    {
        public UmountQuery(IClientSsh client,string device) : base(client)
        {
            CmdString = $"umount {device}";
        }

        protected override bool PaseResult(string result)
        {
            return !result.Contains("umount");
        }
    }
}
