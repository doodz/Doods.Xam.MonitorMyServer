using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.Datas;
using Doods.Synology.Webapi.Std.NewFolder;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoSystemClient
    {
        Task<SystemInfo> GetSystemInfo();
        Task<NetworkInfo> GetNetworkInfo();
        Task<StorageInfo> GetStorageInfo();
        Task<SynologyProcessInfo> GetProcessInfo();
        Task<SynologyUtilizationInfo> GetUtilizationInfo();
        Task<SynologyProcessGroupInfo> GetProcessGroupInfo();
    }

    public class SynoSystemClient : BaseSynoClient, ISynoSystemClient
    {
        public SynoSystemClient(ISynoWebApi client) : base(client)
        {
            ServiceApiName = "SYNO.Core.System";
        }

        public async Task<NetworkInfo> GetNetworkInfo()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "info");
            loginRequest.AddParameter("type", "network");
            loginRequest.AddParameter("sid", _client.Sid);


            var response = await _client.ExecuteAsync<SynologyResponse<NetworkInfo>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }

        public async Task<StorageInfo> GetStorageInfo()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "info");
            loginRequest.AddParameter("type", "storage");
            loginRequest.AddParameter("sid", _client.Sid);


            var response = await _client.ExecuteAsync<SynologyResponse<StorageInfo>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }

        public async Task<SystemInfo> GetSystemInfo()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "info");
            loginRequest.AddParameter("sid", _client.Sid);


            var response = await _client.ExecuteAsync<SynologyResponse<SystemInfo>>(loginRequest).ConfigureAwait(false);
            return response.Data.Data;
        }

        public async Task<SynologyProcessInfo> GetProcessInfo()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName + ".Process");
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "list");


            var response = await _client.ExecuteAsync<SynologyResponse<SynologyProcessInfo>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }

        public async Task<SynologyUtilizationInfo> GetUtilizationInfo()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName + ".Utilization");
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "get");
            loginRequest.AddParameter("type", "current");


            var response = await _client.ExecuteAsync<SynologyResponse<SynologyUtilizationInfo>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }

        public async Task<SynologyProcessGroupInfo> GetProcessGroupInfo()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName + ".ProcessGroup");
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "status");


            var response = await _client.ExecuteAsync<SynologyResponse<SynologyProcessGroupInfo>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }
    }
}