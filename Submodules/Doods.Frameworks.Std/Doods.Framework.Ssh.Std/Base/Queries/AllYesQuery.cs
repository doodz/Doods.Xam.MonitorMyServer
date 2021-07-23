using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Base.Queries
{
    public class AllYesQuery : GenericQuery<bool>
    {
        public AllYesQuery(IClientSsh client, string command) : base(client)
        {
            CmdString = $"yes \"\" | {command}";
        }
    }
}