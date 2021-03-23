using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.Datas;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoShareClient
    {
        Task<SynolgySharesInfo> GetSharedFoldersInfo();
    }

    public class SynoShareClient : BaseSynoClient, ISynoShareClient
    {
        public SynoShareClient(ISynoWebApi client) : base(client)
        {
            Resource = "/entry.cgi";
            ServiceApiName = "SYNO.Core.Share";
        }

        public async Task<SynolgySharesInfo> GetSharedFoldersInfo()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "list");
            loginRequest.AddParameter("shareType", "all");
            loginRequest.AddParameter("sid", _client.Sid);

            var response = await _client.ExecuteAsync<SynologyResponse<SynolgySharesInfo>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }
    }
}