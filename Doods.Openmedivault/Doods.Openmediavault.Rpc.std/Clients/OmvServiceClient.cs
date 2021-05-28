using System.Threading.Tasks;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmediavault.Rpc.Std.Clients
{
    public abstract class OmvServiceClient
    {
        private readonly IRpcClient _client;
        protected readonly RequestType RequestType;
        private OMVVersion _OMVVersions;
        protected string ServiceName;

        public OmvServiceClient(IRpcClient client)
        {
            _client = client;
            if (client != null)
                RequestType = client.RequestType;
        }

        public async Task<T> RunCmd<T>(IRpcRequest request)
        {
            var cmdResult = await _client.ExecuteTaskAsync<T>(request).ConfigureAwait(false);
            return cmdResult;
        }

        protected internal RpcRequest NewRequest(string serviceName, string methodName)
        {
            var rpcRequest = new RpcRequest
            {
                Service = serviceName,
                Method = methodName,
                Params = null,
                Options = null
            };

            return rpcRequest;
        }

        protected internal RpcRequest NewRequest(string methodName)
        {
            return NewRequest(ServiceName, methodName);
        }

        public void SetOMVVersion(OMVVersion version)
        {
            _OMVVersions = version;
        }

        public OMVVersion GetRpcVersion()
        {
            if (_OMVVersions == null) CheckRpcVersionAsync().ConfigureAwait(false);

            return _OMVVersions;
        }

        public async Task<OMVVersion> CheckRpcVersionAsync()
        {
            var request = NewRequest("System", "getInformation");
            request.Options = new Options {Updatelastaccess = false};

            var result = await RunCmd<object>(request);
            return _OMVVersions = OMVVersions.GetVersionFromString(result.ToString());
        }
    }
}