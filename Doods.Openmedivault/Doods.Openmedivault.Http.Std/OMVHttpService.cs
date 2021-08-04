using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using Doods.Framework.Http.Std.Serializers;
using Doods.Framework.Std;
using Doods.Openmediavault.Rpc.Std;
using Doods.Openmediavault.Rpc.Std.Enums;
using Doods.Openmediavault.Rpc.Std.Interfaces;

namespace Doods.Openmedivault.Http.Std
{
    public class OmvHttpService : HttpServiceBase, IRpcClient
    {
        /// <summary>
        /// </summary>
        private readonly IHttpClient _client;

        private readonly string _extension = "png";

        private readonly Requestbuilder _requestbuilder = new Requestbuilder();
        private readonly string[] _typeDate = new string[5] {"hour", "day", "week", "month", "year"};
        private readonly string[] _typeInfo = new string[4] {"df-root", "memory", "load", "cpu-0"};

        public OmvHttpService(ILogger logger, IHttpClient client) : base(logger)
        {
            _client = client;
        }

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

        //df-srv-dev-disk-by-label-OpenMediaVault-hour
        //df-srv-dev-disk-by-id-wwn-0x6001405d8…8fcd44afd863fdf-part1-hour

        /*
         * rrd.php?name=memory-day.png&time=1584971861096
         * rrd.php?name=memory-week.png&time=1584971861096
         * rrd.php?name=memory-month.png&time=1584971861096
         * rrd.php?name=memory-year.png&time=1584971861096
         * rrd.php?name=df-root-hour.png&time=1584971861972
         * rrd.php?name=df-root-day.png&time=1584971861972
         * rrd.php?name=df-root-week.png&time=1584971861972
         * rrd.php?name=df-root-month.png&time=1584971861972
         * rrd.php?name=df-root-year.png&time=1584971861972
         * rrd.php?name=df-srv-dev-disk-by-label-OpenMediaVault-hour.png&time=1584971861976
         * rrd.php?name=df-srv-dev-disk-by-label-LEDISCKC-hour.png&time=1584971861974
         * rrd.php?name=df-srv-dev-disk-by-id-wwn-0x6001405d8…8fcd44afd863fdf-part1-hour.png&time=1584971861973
         * rrd.php?name=load-day.png&time=1584971861095
         * rrd.php?name=cpu-0-hour.png&time=1584971861097
         *
        */
        public Task<IEnumerable<string>> ListRrdFilesAsync()
        {
            var lst = new List<string>();
            foreach (var i in _typeInfo)
            foreach (var d in _typeDate)
                lst.Add($"{i}-{d}.{_extension}");
            return Task.FromResult(lst.AsEnumerable());
        }

        /// <summary>
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<byte[]> GetRrdFileAsync(string filePath)
        {
            var request = _requestbuilder.ToGetRrd(filePath);

            var response = await _client.ExecuteAsync(request).ConfigureAwait(false);
            return response.RawBytes;
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

        public async Task<T> ExecuteRequestAsync<T>(IRpcRequest rpcrequest)
        {
            var request = _requestbuilder.ToHttp(rpcrequest);

            var response = await _client.ExecuteAsync<T>(request).ConfigureAwait(false);
            return response.Data;
        }
    }
}