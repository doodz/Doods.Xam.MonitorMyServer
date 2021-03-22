using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Classes;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmediavault.Rpc.Std.Interfaces;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmedivault.Ssh.Std
{
    internal class DefaultSshRequest : OmvRequestBase
    {
        public DefaultSshRequest(string requestString) : base(requestString)
        {
        }
    }

    public class OmvSshService : APIServiceBase, IRpcClient
    {
        /// <summary>
        /// </summary>
        private readonly OmvRpc _client;

        private readonly Requestbuilder _requestbuilder = new Requestbuilder();

        public OmvSshService(ILogger logger, IConnection connection)
        {
            Connection = connection;
            _client = new OmvRpc(logger, Connection);
        }

        public RequestType RequestType => RequestType.ssh;


        public Task<bool> LoginAsync(string username, string password)
        {
            var b = _client.Connect();
            return Task.FromResult(b);
        }

        public async Task<IEnumerable<string>> ListRrdFilesAsync()
        {
            var sshrequest = new DefaultSshRequest("ls /var/lib/openmediavault/rrd");
            var response = await _client.ExecuteTaskAsync<string>(sshrequest).ConfigureAwait(false);
            //var result = await RunCommand("ls  /var/lib/openmediavault/rrd");
            return response.Content.Split(new[] {"\r\n", "\n"}, StringSplitOptions.RemoveEmptyEntries);
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