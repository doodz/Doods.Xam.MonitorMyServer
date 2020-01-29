using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using RestSharp;

namespace Doods.Openmedivault.Http.Std
{
    public class OmvRpc : RestClientBase
    {
        public OmvRpc(IConnection connection) : base(connection)
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        protected override void AddHeaders(IRestRequest request)
        {
            request.AddHeader("accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept-Encoding", "gzip, deflate");
        }

        protected override string DeserializeError(IRestResponse response)
        {
            return response.Content;
        }
    }
}