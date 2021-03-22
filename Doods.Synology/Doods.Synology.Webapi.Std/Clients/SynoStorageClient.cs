using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.Datas;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoStorageClient
    {
        Task<SynologyStorageInfo> GetFullStorageInfo();
    }

    public class SynoStorageClient : BaseSynoClient, ISynoStorageClient
    {
        public SynoStorageClient(ISynoWebApi client) : base(client)
        {
            Resource = "/entry.cgi";
            ServiceApiName = "SYNO.Storage.CGI.Storage";
        }

        public async Task<SynologyStorageInfo> GetFullStorageInfo()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "load_info");
            loginRequest.AddParameter("sid", _client.Sid);

            var response = await _client.ExecuteAsync<SynologyResponse<SynologyStorageInfo>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }
    }
}