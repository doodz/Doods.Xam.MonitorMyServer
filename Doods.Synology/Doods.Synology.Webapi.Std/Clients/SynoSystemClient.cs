using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.NewFolder;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoSystemClient
    {
        Task<SystemInfo> GetSystemInfo();
        Task<NetworkInfo> GetNetworkInfo();
        Task<StorageInfo> GetStorageInfo();
    }
    class SynoSystemClient: BaseSynoClient, ISynoSystemClient
    {
        public SynoSystemClient(ISynoWebApi client):base(client)
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


            var response = await _client.ExecuteAsync<SynologyResponse<NetworkInfo>>(loginRequest).ConfigureAwait(false);
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


            var response = await _client.ExecuteAsync<SynologyResponse<StorageInfo>>(loginRequest).ConfigureAwait(false);
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

    }
}
