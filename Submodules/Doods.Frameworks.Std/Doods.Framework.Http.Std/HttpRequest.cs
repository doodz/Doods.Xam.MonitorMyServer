using Doods.Framework.Http.Std.Serializers;
using RestSharp;

namespace Doods.Framework.Http.Std
{
    public class HttpRequest : RestRequest
    {
        public HttpRequest(string resource, Method method) : base(resource, method)
        {
            JsonSerializer = new NewtonsoftJsonSerializer();
        }
    }
}