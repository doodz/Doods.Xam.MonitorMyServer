using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using Doods.Framework.Std;
using Doods.Webmin.Webapi.Std;
using Doods.Webmin.Webapi.Std.Clients;
using Doods.Webmin.Webapi.Std.Datas;
using Doods.Xam.MonitorMyServer.Data.Nas;
using Doods.Xam.MonitorMyServer.Services;

namespace Doods.Synology.Webapi.Std
{
    public interface IWebminCgiService : IPackageUpdates,IWebminStatClient, IWebminLoginClient, IPackageUpdatesClient
    {
    }

    public class WebminCgiService : HttpServiceBase, IWebminCgiService
    {
        private readonly IWebminApi _client;
        private readonly IMapper _mapper;
        private readonly IWebminLoginClient _webminLoginClient;

        private readonly IPackageUpdatesClient _packageUpdatesClient;
        private readonly IWebminStatClient _webminStatClient;

        public WebminCgiService(ILogger logger, IConnection connection, IMapper mapper) : base(logger)
        {
            Connection = connection;
            _client = new WebminApi(connection);
            _mapper = mapper;
            _webminLoginClient = new WebminLoginClient(_client);
            _webminStatClient = new WebminStatsCleint(_client);
            _packageUpdatesClient = new PackageUpdatesClient(_client);
        }

        public Task<Stats> GetStats()
        {
            return _webminStatClient.GetStats();
        }

        public Task<bool> LoginAsync(string username, string password)
        {
            return _webminLoginClient.LoginAsync(username, password);
        }

        public void LogOut()
        {
            _webminLoginClient.LogOut();
        }

        public Task<string> GetUpdates()
        {
            return _packageUpdatesClient.GetUpdates();
        }

        public async Task<IEnumerable<Package>> GetPackages()
        {
           var result = await GetUpdates();
            

           return new List<Package>();
        }

        public Task UpdatePackages(IEnumerable<Package> packages)
        {
            throw new System.NotImplementedException();
        }
    }
}