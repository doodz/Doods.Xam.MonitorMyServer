using System;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using RestSharp;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoWebApi : IHttpClient
    {
        string Sid { get; set; }
        DateTime LoggedInTime { get; set; }
        IRestResponse Execute(IRestRequest request);
    }

    public class SynologyApi : RestClientBase, ISynoWebApi
    {
        public SynologyApi(IConnection connection) : base(connection)
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public string Sid { get; set; }
        public DateTime LoggedInTime { get; set; }

        protected override void AddHeaders(IRestRequest request)
        {
        }

        protected override string DeserializeError(IRestResponse response)
        {
            return response.Content;
        }
    }
}