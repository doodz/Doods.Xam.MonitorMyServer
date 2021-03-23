using System;
using System.Threading.Tasks;
using Doods.Webmin.Webapi.Std.Datas;
using RestSharp;

namespace Doods.Webmin.Webapi.Std.Clients
{
    public class WebminLoginClient : BaseWebminClient, IWebminLoginClient
    {
        public WebminLoginClient(IWebminApi client) : base(client)
        {
            Resource = "/session_login.cgi";
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            if (await Ping() == false) return false;
            //format=cookie
            var loginRequest = new WebminRestRequest(Resource);
            loginRequest.Method = Method.POST;
            loginRequest.AddParameter("user", username);
            loginRequest.AddParameter("pass", password);


            try
            {
                //Referer
                //var response = await _client.ExecuteAsync<SynologyResponse<SynoLoginInfo>>(loginRequest);
                var response = await _client.ExecuteAsync(loginRequest);

                return response.IsSuccessful && string.IsNullOrWhiteSpace( response.Content);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return false;
        }

        public void LogOut()
        {
            var request = new WebminRestRequest(Resource);
            request.AddParameter("logout", "1");
            _client.Execute(request);
        }


        private async Task<bool> Ping()
        {
            var loginRequest2 = new WebminRestRequest(Resource);
            var response = await _client.ExecuteAsync(loginRequest2);
            return response.IsSuccessful;
        }


        private async Task<Stats> Get()
        {
            var loginRequest2 = new WebminRestRequest("/stats.cgi");
            loginRequest2.AddParameter("xhr-stats", "general");
            var response = await _client.ExecuteAsync<Stats>(loginRequest2);
            return response.Data;
        }
    }
}