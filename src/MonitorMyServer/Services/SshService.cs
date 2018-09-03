using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Ssh.Std;
using Doods.Framework.Std;

namespace Doods.Xam.MonitorMyServer.Services
{
    public class SshService : SshServiceBase
    {

        public SshService(IConnection connection,ILogger logger):base(logger)
        {
            Connection = connection;
        }

        public void Init()
        {
           var res = Connect();
        }
    }
}
