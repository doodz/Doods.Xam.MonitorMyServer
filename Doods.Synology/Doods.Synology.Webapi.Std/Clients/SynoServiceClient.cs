using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.Datas;
using RestSharp;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoServiceClient
    {
        Task<SynologyServiceInfo> GetServicesInfo();
    }

    public class SynoServiceClient : BaseSynoClient, ISynoServiceClient
    {
        public SynoServiceClient(ISynoWebApi client) : base(client)
        {
            Resource = "/entry.cgi";
            ServiceApiName = "SYNO.Core.Service";
        }

        public async Task<SynologyServiceInfo> GetServicesInfo()
        {
            var array = new JsonArray();
            array.Add("status");
            array.Add("allow_control");

            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "get");
            loginRequest.AddParameter("additional", array);
            loginRequest.AddParameter("sid", _client.Sid);


            var response = await _client.ExecuteAsync<SynologyResponse<SynologyServiceInfo>>(loginRequest)
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