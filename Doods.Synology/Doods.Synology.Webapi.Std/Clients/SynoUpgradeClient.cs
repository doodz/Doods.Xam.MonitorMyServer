using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.NewFolder;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoUpgradeClient
    {
        Task<SynologyUpgradeStatus> GetUpgradeStatus();
    }

    public class SynoUpgradeClient : BaseSynoClient, ISynoUpgradeClient
    {
        public SynoUpgradeClient(ISynoWebApi client) : base(client)
        {
            Resource = "/entry.cgi";
            ServiceApiName = "SYNO.Core.Upgrade";

        }

        public async Task<SynologyUpgradeStatus> GetUpgradeStatus()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "status");
            loginRequest.AddParameter("sid", _client.Sid);

            var response = await _client.ExecuteAsync<SynologyResponse<SynologyUpgradeStatus>>(loginRequest).ConfigureAwait(false);
            return response.Data.Data;
        }
    }
}
