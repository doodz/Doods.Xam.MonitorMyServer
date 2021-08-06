using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.NewFolder;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoDesktopDefsClient
    {
        
    }
    public class SynoDesktopDefsClient : BaseSynoClient, ISynoDesktopDefsClient
    {
        public SynoDesktopDefsClient(ISynoWebApi client) : base(client)
        {
            Resource = "/query.cgi";
            ServiceApiName = "SYNO.Core.Desktop.Defs";
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