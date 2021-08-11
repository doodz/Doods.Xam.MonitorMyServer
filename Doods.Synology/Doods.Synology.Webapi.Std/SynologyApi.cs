using System;
using System.Collections.Generic;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using Doods.Synology.Webapi.Std.NewFolder;
using RestSharp;

namespace Doods.Synology.Webapi.Std
{
    public interface ISynoWebApi : IHttpClient
    {
        string Synotoken { get; set; }
        string Sid { get; set; }
        DateTime LoggedInTime { get; set; }
        IRestResponse Execute(IRestRequest request);
        IDictionary<string, SynologyApiServicesInfo> ApiInfo { get; set; }
    }

    public class SynologyApi : RestClientBase, ISynoWebApi
    {
        public SynologyApi(IConnection connection) : base(connection)
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        public string Sid { get; set; }
        public string Synotoken { get; set; }
        public DateTime LoggedInTime { get; set; }
        public IDictionary<string, SynologyApiServicesInfo> ApiInfo { get; set; }


        protected override void AddHeaders(IRestRequest request)
        {
        }

        protected override string DeserializeError(IRestResponse response)
        {
            return response.Content;
        }
    }
}