using System.Threading.Tasks;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvRrdClient : OmvServiceClient
    {
      

        public OmvRrdClient(IRpcClient client) : base(client)
        {
            ServiceName = "Rrd";
        }

        public Task<string> GenerateRdd()
        {
            var request = NewRequest("generate");
           
            var result = RunCmd<string>(request);

            return result;
        }
    }
}