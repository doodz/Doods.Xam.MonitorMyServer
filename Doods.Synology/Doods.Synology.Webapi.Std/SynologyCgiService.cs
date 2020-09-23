using System.Collections.Generic;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using Doods.Framework.Std;
using Doods.Synology.Webapi.Std.Datas;
using Doods.Synology.Webapi.Std.NewFolder;

namespace Doods.Synology.Webapi.Std
{
    public class SynologyCgiService : HttpServiceBase, ISynologyCgiService
    {
       
        private readonly ISynoWebApi _client;
        private readonly ISynoSystemClient _synoSystemClient;
        private readonly ISynoAuthClient _synoAuthClient;
        private readonly ISynoUpgradeClient _synoUpgradeClient;
        private readonly ISynoInfoClient _synoInfoClient;
        private readonly ISynoStorageClient _synoStorageClient;
        public SynologyCgiService(ILogger logger, IConnection connection) : base(logger)
        {
            Connection = connection;
            _client = new SynologyApi(connection);

            _synoSystemClient = new SynoSystemClient(_client);
            _synoAuthClient = new SynoAuthClient(_client);
            _synoInfoClient = new SynoInfoClient(_client);
            _synoUpgradeClient = new SynoUpgradeClient(_client);
            _synoStorageClient = new SynoStorageClient(_client);
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

        public Task<SynologyUpgradeStatus> GetUpgradeStatus()
        {
            return _synoUpgradeClient.GetUpgradeStatus();
        }

        public Task<NetworkInfo> GetNetworkInfo()
        {
            return _synoSystemClient.GetNetworkInfo();
        }

        public Task<StorageInfo> GetStorageInfo()
        {
            return _synoSystemClient.GetStorageInfo();
        }

        public Task<SynologyStorageInfo> GetFullStorageInfo()
        {
            return _synoStorageClient.GetFullStorageInfo();
        }
    }
}
