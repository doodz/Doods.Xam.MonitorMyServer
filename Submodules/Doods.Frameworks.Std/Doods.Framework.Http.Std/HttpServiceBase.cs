using Doods.Framework.ApiClientBase.Std.Classes;
using Doods.Framework.Std;
using RestSharp;

namespace Doods.Framework.Http.Std
{
    public class HttpServiceBase : APIServiceBase
    {
        private RestClient _client;

        public HttpServiceBase(ILogger logger)
        {
            Logger = logger;
        }

        public ILogger Logger { get; }

        //protected virtual IRestClient GetHttpClient()
        //{

        //   return _client ?? (_client = new RestClientBase(Connection));
        //}
    }
}