using Doods.Framework.Ssh.Std.Base.Queries;
using Doods.Framework.Ssh.Std.Interfaces;

namespace Doods.Framework.Ssh.Std.Queries
{
    public class CheckCarrierQuery : GenericQuery<bool>
    {
        private readonly string _interfaceName;
        public CheckCarrierQuery(IClientSsh client,string interfaceName) : base(client)
        {
            _interfaceName = interfaceName;
            CmdString = "cat /sys/class/net/" + interfaceName + "/carrier";
        }

        protected override bool PaseResult(string result)
        {

            if (result.Contains("1"))
            {
                Client.Logger.Debug($"{_interfaceName} has a carrier up and running.");
                return true;
            }
            else
            {
                Client.Logger.Debug($"{_interfaceName} has no carrier.");
                return false;
            }
        }
    }
}
