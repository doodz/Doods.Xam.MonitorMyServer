using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.Datas;
using RestSharp;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoFileStationClient
    {
        
    }
    public class SynoFileStationClient : BaseSynoClient, ISynoFileStationClient
    {
        public SynoFileStationClient(ISynoWebApi client) : base(client)
        {
            Resource = "/entry.cgi";
            ServiceApiName = "SYNO.FileStation.List";
        }

        public async Task<TempShare> ListileStationFolder(string folderPath)
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "2");
            loginRequest.AddParameter("method", "list");
            loginRequest.AddParameter("folder_path", folderPath);
            loginRequest.AddParameter("filetype", "all");
            loginRequest.AddArrayParameter("additional", 
                "real_path", "size", "owner", "time", "perm", "type", "mount_point_type", "description", "indexed" );
            loginRequest.AddParameter("sort_by", "mtime");
            loginRequest.AddParameter("check_dir", true);
            loginRequest.AddParameter("action", "list");
            loginRequest.AddParameter("limit", 1000);
            loginRequest.AddParameter("offset", 0);
            loginRequest.AddParameter("sid", _client.Sid);

            var response = await _client.ExecuteAsync<SynologyResponse<TempShare>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }

        public async Task<TempShare> GetFileStationList()
        {
            var array = new JsonArray();
            
            array.Add("real_path");
            array.Add("owner");
            array.Add("time");
            array.Add("perm");
            array.Add("mount_point_type");
            array.Add("sync_share");
            array.Add("volume_status");
            array.Add("indexed");

            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "2");
            loginRequest.AddParameter("method", "list_share");
            loginRequest.AddParameter("node", "fm_root");
            loginRequest.AddParameter("enum_cluster", true);
            loginRequest.AddParameter("additional", array);
            loginRequest.AddParameter("sort_by", "name");
            loginRequest.AddParameter("check_dir", true);
            loginRequest.AddParameter("filetype", "dir");
            
            loginRequest.AddParameter("sid", _client.Sid);

            var response = await _client.ExecuteAsync<SynologyResponse<TempShare>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }

        public async Task<TempShare> GetSharedFolders()
        {
            var array = new JsonArray();
            array.Add("indexed");

            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "2");
            loginRequest.AddParameter("method", "list_share");
            loginRequest.AddParameter("sort_direction", "ASC");
            loginRequest.AddParameter("sort_by", "name");
            loginRequest.AddParameter("additional", array);

            loginRequest.AddParameter("sid", _client.Sid);

            var response = await _client.ExecuteAsync<SynologyResponse<TempShare>>(loginRequest)
                .ConfigureAwait(false);
            return response.Data.Data;
        }
    }
}