using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Ssh.Std;
using Doods.Framework.Std;

namespace Doods.Openmedivault.Ssh.Std
{
    public class OmvRpc : SshServiceBase
    {
        public OmvRpc(ILogger logger, IConnection connection) : base(logger)
        {
            Connection = connection;
        }
    }
}