using Doods.Framework.ApiClientBase.Std.Models;
using Doods.Framework.Ssh.Std;
using Doods.Framework.Ssh.Std.Interfaces;
using Doods.Framework.Std;

namespace Doods.Xam.MonitorMyServer.Services
{

    public interface ISshService:IClientSsh
    {
        void Init(IConnection connection, bool andConnect);
    }
    public class SshService : SshServiceBase, ISshService
    {

        public SshService(ILogger logger):base(logger)
        {
            
        }

        public void Init(IConnection connection,bool andConnect = true)
        {
            Connection = connection;
            if (andConnect)
                Connect();
        }


       
    }
}
