using System.Threading.Tasks;
using Doods.Webmin.Webapi.Std.Datas;

namespace Doods.Webmin.Webapi.Std.Clients
{
   


    public interface IWebminStatClient
    {
        Task<Stats> GetStats();
    }

    public class WebminStatsCleint : BaseWebminClient, IWebminStatClient
    {
        public WebminStatsCleint(IWebminApi client) : base(client)
        {
            Resource = "/stats.cgi";
        }

        public async Task<Stats> GetStats()
        {
            var loginRequest2 = new WebminRestRequest(Resource);
            loginRequest2.AddParameter("xhr-stats", "general");
            var response = await _client.ExecuteAsync<Stats>(loginRequest2);
            return response.Data;
        }
    }
}