using RestSharp;
using RestSharp.Authenticators;

namespace Doods.Framework.Http.Std.Authentication
{
    internal class SimpleHttpHeaderAuthenticator : IAuthenticator
    {
        private readonly string _authHeader;
        private readonly string _password;

        private readonly string _username;

        public SimpleHttpHeaderAuthenticator(string username, string password)
        {
            //var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
            _username = username;
            _password = password;
            _authHeader = $"{username}:{password}";
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            //request.AddParameter(_username, _password);
            request.AddHeader(_username, _password);
        }
    }
}