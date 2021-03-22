using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.Datas;
using RestSharp;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoPackageClient
    {
        Task<SynologyPackageInfo> GetPackagesInfo();
    }

    public class SynoPackageClient : BaseSynoClient, ISynoPackageClient
    {
        public SynoPackageClient(ISynoWebApi client) : base(client)
        {
            Resource = "/entry.cgi";
            ServiceApiName = "SYNO.Core.Package";
        }

        public async Task<SynologyPackageInfo> GetPackagesInfo()
        {
            var array = new JsonArray();
            array.Add("startable");
            array.Add("dependent_packages");

            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "list");
            loginRequest.AddParameter("additional", array);
            loginRequest.AddParameter("sid", _client.Sid);


            var response = await _client.ExecuteAsync<SynologyResponse<SynologyPackageInfo>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }
        //public async Task<SynologyEntriesInfo> GetServicesInfo()
        //{
        //    var loginRequest = new SynologyRestRequest(Resource);
        //    loginRequest.AddParameter("api", ServiceApiName);
        //    loginRequest.AddParameter("version", "1");
        //    loginRequest.AddParameter("method", "request");
        //    loginRequest.AddParameter("stop_when_error", "false");
        //    loginRequest.AddParameter("mode", "sequential");
        //    loginRequest.AddParameter("sid", _client.Sid);

        //    var response = await _client.ExecuteAsync<SynologyResponse<SynologyEntriesInfo>>(loginRequest).ConfigureAwait(false);
        //    return response.Data.Data;
        //}
    }
}