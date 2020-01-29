using System.Threading.Tasks;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public abstract class OmvServiceClient
    {
        private  readonly IRpcClient _client;
        protected internal string ServiceName;
       public OmvServiceClient(IRpcClient client)
        {
            _client = client;
        }

        public async Task<T> RunCmd<T>(IRpcRequest request)
        {
            var cmdResult = await _client.ExecuteTaskAsync<T>(request).ConfigureAwait(false);
            return cmdResult;
        }

        protected internal RpcRequest NewRequest(string methodName)
        {
            var rpcRequest = new RpcRequest
            {
                Service = ServiceName,
                Method = methodName,
                Params = null,
                Options = null
            };

            return rpcRequest;
        }

    }
}