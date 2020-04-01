using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using RestSharp;

namespace Doods.Synology.Webapi.Std
{

    public interface ISynoWebApi: IHttpClient
    {
        IRestResponse Execute(IRestRequest request);
        string Sid { get;  set; }
        DateTime LoggedInTime { get; set; }
    }
    public class SynologyApi : RestClientBase , ISynoWebApi
    {
        
        public SynologyApi(IConnection connection) : base(connection)
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }

        protected override void AddHeaders(IRestRequest request)
        {
           
        }

        protected override string DeserializeError(IRestResponse response)
        {
            return response.Content;
        }

        public string Sid { get;  set; }
        public DateTime LoggedInTime { get;  set; }
    }
}
