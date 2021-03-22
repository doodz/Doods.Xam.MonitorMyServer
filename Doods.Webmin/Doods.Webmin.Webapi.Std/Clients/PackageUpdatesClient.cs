using System.Threading.Tasks;

namespace Doods.Webmin.Webapi.Std.Clients
{
    public interface IPackageUpdatesClient
    {
        Task<string> GetUpdates();
    }
    public class PackageUpdatesClient : BaseWebminClient, IPackageUpdatesClient
    {

        public PackageUpdatesClient(IWebminApi client) : base(client)
        {
            Resource = "/package-updates/index.cgi";
        }

        public async Task<string> GetUpdates()
        {
            var loginRequest2 = new WebminRestRequest(Resource);
            loginRequest2.AddParameter("mode", "updates");
            var response = await _client.ExecuteAsync(loginRequest2);
            return response.Content;
        }
    }
}