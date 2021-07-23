using RestSharp;
using RestSharp.Authenticators;

namespace Doods.Framework.Http.Std.Authentication
{
    internal class AnonymousAuthenticator : IAuthenticator
    {
        public void Authenticate(IRestClient client, IRestRequest request)
        {
        }
    }
}