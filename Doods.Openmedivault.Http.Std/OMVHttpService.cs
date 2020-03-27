using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using Doods.Framework.Http.Std.Serializers;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmedivault.Ssh.Std.Requests;

namespace Doods.Openmedivault.Http.Std
{
    public class OmvHttpService : HttpServiceBase, IRpcClient
    {
        /// <summary>
        /// </summary>
        private readonly IHttpClient _client;

        private readonly Requestbuilder _requestbuilder = new Requestbuilder();

        public OmvHttpService(ILogger logger, IConnection connection) : base(logger)
        {
            _client = new OmvRpc(connection);
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var loginRequest = new RpcRequest
            {
                Service = "Session",
                Method = "login",
                Params = new ParamsLogin {Username = username, Password = password},
                Options = null
            };

            var data = await ExecuteRequestAsync<RpcResponse<LoginResult>>(loginRequest).ConfigureAwait(false);
            if (data.Error != null)
                return false;
            return data.Response.Authenticated;
        }

        public RequestType RequestType => RequestType.http;

        public async Task<T> ExecuteTaskAsync<T>(IRpcRequest rpcrequest)
        {
            var request = _requestbuilder.ToHttp(rpcrequest);
            var response = await _client.ExecuteAsync<RpcResponse<T>>(request).ConfigureAwait(false);
            return response.Data.Response;
        }

        public void SetHandlers(NewtonsoftJsonSerializer serializer)
        {
            var tmp = (OmvRpc) _client;
            tmp.ClearHandlers();
            tmp.AddHandler("application/json", () => serializer);
            tmp.AddHandler("text/json", () => serializer);
            tmp.AddHandler("text/plain", () => serializer);
            tmp.AddHandler("text/x-json", () => serializer);
            tmp.AddHandler("text/javascript", () => serializer);
            tmp.AddHandler("*+json", () => serializer);
        }

        public async Task<IEnumerable<byte[]>> GetRrdFilesAsync(IEnumerable<string> filesPaths)
        {
            var lst = new List<byte[]>();

            foreach (var filePath in filesPaths)
            {
                var result = await GetRrdFileAsync(filePath);
                lst.Add(result);
            }

            return lst;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<byte[]> GetRrdFileAsync(string filePath)
        {
            var request = _requestbuilder.ToGetRrd(filePath);

            var response = await _client.ExecuteAsync(request).ConfigureAwait(false);
            return response.RawBytes;
        }

        public async Task<T> ExecuteRequestAsync<T>(IRpcRequest rpcrequest)
        {
            var request = _requestbuilder.ToHttp(rpcrequest);
            var response = await _client.ExecuteAsync<T>(request).ConfigureAwait(false);
            return response.Data;
        }
    }
}