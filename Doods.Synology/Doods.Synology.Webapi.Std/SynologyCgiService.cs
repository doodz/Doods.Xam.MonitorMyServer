using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using Doods.Framework.Std;
using Doods.Synology.Webapi.Std.NewFolder;

namespace Doods.Synology.Webapi.Std
{
    public class SynologyCgiService : HttpServiceBase, ISynologyCgiService
    {
       
        private readonly ISynoWebApi _client;
        private readonly SynoSystemClient _synoSystemClient;
        private readonly SynoAuthClient _synoAuthClient;
        private readonly SynoInfoClient _synoInfoClient;

        public SynologyCgiService(ILogger logger, IConnection connection) : base(logger)
        {
            _client = new SynologyApi(connection);

            _synoSystemClient = new SynoSystemClient(_client);
            _synoAuthClient = new SynoAuthClient(_client);
            _synoInfoClient = new SynoInfoClient(_client);

        }


        public Task<IDictionary<string, SynologyApiServicesInfo>> GetSynologyApiServicesInfo()
        {
            return _synoInfoClient.GetSynologyApiServicesInfo();
        }

        public Task<bool> LoginAsync(string username, string password)
        {
            return _synoAuthClient.LoginAsync(username, password);
        }

        public void LogOut()
        {
             _synoAuthClient.LogOut();
        }

        public Task<SystemInfo> GetSystemInfo()
        {
            return _synoSystemClient.GetSystemInfo();
        }

        public Task<NetworkInfo> GetNetworkInfo()
        {
            return _synoSystemClient.GetNetworkInfo();
        }
    }
}
