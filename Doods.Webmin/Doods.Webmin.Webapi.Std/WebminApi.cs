using System;
using System.Globalization;
using System.Threading.Tasks;
using Doods.Framework.ApiClientBase.Std.Interfaces;
using Doods.Framework.Http.Std;
using Doods.Framework.Http.Std.Serializers;
using Doods.Webmin.Webapi.Std.Classes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RestSharp;

namespace Doods.Webmin.Webapi.Std
{
    public interface IWebminApi : IHttpClient
    {
        IRestResponse Execute(IRestRequest request);
    }

    public class WebminApi : RestClientBase, IWebminApi
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                DiskElementConverter.Singleton,
                new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal}
            }
        };

        private Uri _last;


        public WebminApi(IConnection connection) : base(connection,
            new NewtonsoftJsonSerializer(JsonSerializer.Create(Settings)))
        {
            RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }


        public async Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            var t = await base.ExecuteAsync(request);

            if (t.IsSuccessful) _last = t.ResponseUri;

            return t;
        }

        public async Task<IRestResponse<T>> ExecuteAsync<T>(IRestRequest request)
        {
            var t = await base.ExecuteAsync<T>(request);

            if (t.IsSuccessful) _last = t.ResponseUri;

            return t;
        }

        protected override void AddHeaders(IRestRequest request)
        {
            request.AddHeader("accept", "*/*");

            request.AddHeader("Referer", _last != null ? _last.ToString() : BuildUri(request).ToString());
        }

        protected override string DeserializeError(IRestResponse response)
        {
            return response.Content;
        }
    }
}