using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class UpdateQuery : GenericQuery<bool>

    {
        private static readonly string Query = "sudo apt-get update";

        public UpdateQuery(IClientSsh client) : base(client)
        {
            CmdString = Query;
        }

        protected override bool PaseResult(string result)
        {
            return true;
            //var res = result.Split('\n').Where(r => !string.IsNullOrWhiteSpace(r) && !string.IsNullOrEmpty(r));
            //return Parse(res);
        }

    }
}
