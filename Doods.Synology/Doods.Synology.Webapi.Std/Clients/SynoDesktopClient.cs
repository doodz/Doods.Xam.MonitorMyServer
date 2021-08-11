using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.NewFolder;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoDesktopClient
    {
        
    }
    public class SynoDesktopClient : BaseSynoClient, ISynoDesktopClient
    {
        public SynoDesktopClient(ISynoWebApi client) : base(client)
        {
            Resource = "/query.cgi";
            ServiceApiName = "SYNO.Core.Desktop";
        }

        public async Task<SynologyUserServices> GetUserServices()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName+".Initdata");
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "get_user_service");
            loginRequest.AddParameter("SynoToken", _client.Synotoken);
            loginRequest.AddParameter("sid", _client.Sid);

            var t = loginRequest.Timeout;
            var response = await _client.ExecuteAsync<SynologySimpleResponse<SynologyUserServices>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }

        //public async Task<IDictionary<string, SynologyApiServicesInfo>> GetSynologyDesktopDefs()
        //{
        //    var loginRequest = new SynologyRestRequest(Resource);
        //    loginRequest.AddParameter("api", ServiceApiName);
        //    loginRequest.AddParameter("version", "1");
        //    loginRequest.AddParameter("method", "getjs");



        //    var response = await _client
        //        .ExecuteAsync<SynologyResponse<Dictionary<string, SynologyApiServicesInfo>>>(loginRequest)
        //        .ConfigureAwait(false);
        //    return response.Data.Data;
        //}
    }
}