using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Classes;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmedivault.Ssh.Std
{
    public class OmvSshService : APIServiceBase, IRpcClient
    {
        public RequestType RequestType => RequestType.ssh;
        /// <summary>
        /// </summary>
        private readonly OmvRpc _client;

        private readonly Requestbuilder _requestbuilder = new Requestbuilder();

        public OmvSshService(ILogger logger, IConnection connection)
        {
            Connection = connection;
            _client = new OmvRpc(logger, Connection);
        }


        public Task<bool> LoginAsync(string username, string password)
        {
            var b = _client.Connect();
            return Task.FromResult(b);
        }

        public Task<IEnumerable<byte[]>> GetRrdFilesAsync(IEnumerable<string> filesPaths)
        {
            return _client.GetFilesAsync(filesPaths);
        }

        public Task<byte[]> GetRrdFileAsync(string filePath)
        {
           return _client.GetFileAsync(filePath);
        }

        public async Task<T> ExecuteTaskAsync<T>(IRpcRequest rpcrequest)
        {
            var request = _requestbuilder.ToSsh(rpcrequest);
            var response = await _client.ExecuteTaskAsync<T>(request).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<T> ExecuteRequestAsync<T>(IRpcRequest rpcrequest)
        {
            var request = _requestbuilder.ToSsh(rpcrequest);
            var response = await _client.ExecuteTaskAsync<T>(request).ConfigureAwait(false);
            return response.Data;
        }
      
    }
}