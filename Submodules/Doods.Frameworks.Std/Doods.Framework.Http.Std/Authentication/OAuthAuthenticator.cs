using Doods.Framework.ApiClientBase.Std.Authentication;

namespace Doods.Framework.Http.Std.Authentication
{
    public class OAuthAuthenticator
    {
        private const string TokenUrl = "https://toto.org/site/oauth2/access_token";
        private const string TokenType = "Bearer";

        public OAuthAuthenticator(Credentials credentials)
        {
        }
    }
}