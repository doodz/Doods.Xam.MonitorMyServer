﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Synology.Webapi.Std.NewFolder;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoInfoClient
    {
        Task<IDictionary<string, SynologyApiServicesInfo>> GetSynologyApiServicesInfo();
    }
    class SynoInfoClient: BaseSynoClient
    {
        public SynoInfoClient(ISynoWebApi client) : base(client)
        {
            Resource = "/query.cgi";
            ServiceApiName = "SYNO.API.Info";

        }

        public async Task<IDictionary<string, SynologyApiServicesInfo>> GetSynologyApiServicesInfo()
        {
            var loginRequest = new SynologyRestRequest(Resource);
            loginRequest.AddParameter("api", ServiceApiName);
            loginRequest.AddParameter("version", "1");
            loginRequest.AddParameter("method", "query");
            loginRequest.AddParameter("query", "all");
            loginRequest.AddParameter("sid", _client.Sid);


            var response = await _client.ExecuteAsync<SynologyResponse<Dictionary<string, SynologyApiServicesInfo>>>(loginRequest).ConfigureAwait(false);
            return response.Data.Data;
        }

    }

    class SynoUpgradeClient : BaseSynoClient
    {
        public SynoUpgradeClient(ISynoWebApi client) : base(client)
        {
            Resource = "/entry.cgi";
            ServiceApiName = "SYNO.Core.Upgrade";

        }

        public async Task<SynologyUpgradeStatus> GetStatus()
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