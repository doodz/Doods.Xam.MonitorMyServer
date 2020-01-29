using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.std.Data.V4;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public class OmvExecClient : OmvServiceClient
    {
        public OmvExecClient(IRpcClient client) : base(client)
        {
            ServiceName = "Exec";
        }

        public Task<Output<T>> GetOutput<T>(string filename, long pos)
        {

            var request = NewRequest("getOutput");
            request.Params = new { filename,pos };
            var result = RunCmd<Output<T>>(request);
            return result;
        }
    }
}