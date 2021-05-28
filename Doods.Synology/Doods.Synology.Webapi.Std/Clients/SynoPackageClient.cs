using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.Datas;
using RestSharp;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoPackageClient
    {
        Task<SynologyPackageInfo> GetPackagesInfo();
        //Task<SynologyPackageServerInfo> GetPackagesServerInfo();
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
            var loginRequest = new SynologyRestRequest(Resource);
           
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "list");
            loginRequest.AddArrayParameter("additional", "startable", "dependent_packages", "installed_info", "description", "description_enu");
            loginRequest.AddParameter("sid", _client.Sid);


            var response = await _client.ExecuteAsync<SynologyResponse<SynologyPackageInfo>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }
        //public async Task<SynologyPackageServerInfo> GetPackagesServerInfo()
        //{
        //    var loginRequest = new SynologyRestRequest(Resource);

        //    loginRequest.AddParameter("api", ServiceApiName+".Server");
        //    loginRequest.AddParameter("version", "2");
        //    loginRequest.AddParameter("method", "list");
        //    loginRequest.AddParameter("blforcereload", false);
        //    loginRequest.AddParameter("blloadothers", false);
            
                
        //    loginRequest.AddParameter("sid", _client.Sid);


        //    var response = await _client.ExecuteAsync<SynologyResponse<SynologyPackageServerInfo>>(loginRequest)
        //        .ConfigureAwait(false);
        //    return response.Data.Data;
        //}
        //additional: ["description","description_enu","beta",
        //"distributor","distributor_url","maintainer",
        //"maintainer_url","dsm_apps","dsm_app_launch_name",
        //"report_beta_url","support_center","startable","installed_info",
        //"support_url","is_uninstall_pages","install_type","autoupdate",
        //"silent_upgrade","installing_progress",
        //"ctl_uninstall","updated_at","status","url"]
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