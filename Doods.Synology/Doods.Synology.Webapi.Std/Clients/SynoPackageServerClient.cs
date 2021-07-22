using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.Datas;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoPackageServerClient
    {
        Task<SynologyPackageServer> GetPackagesServerInfo();
    }
    public class SynoPackageServerClient : BaseSynoClient, ISynoPackageServerClient
    {
        public SynoPackageServerClient(ISynoWebApi client) : base(client)
        {
            Resource = "/entry.cgi";
            ServiceApiName = "SYNO.Core.Package.Server";
        }
        public async Task<SynologyPackageServer> GetPackagesServerInfo()
        {
          

            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "2");
            loginRequest.AddParameter("blforcereload", false);
            loginRequest.AddParameter("blloadothers", false);
            loginRequest.AddParameter("method", "list");
            loginRequest.AddParameter("sid", _client.Sid);


            var response = await _client.ExecuteAsync<SynologyResponse<SynologyPackageServer>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }
    }
}