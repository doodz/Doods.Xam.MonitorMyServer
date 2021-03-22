using RestSharp;

namespace Doods.Webmin.Webapi.Std
{
    internal class WebminRestRequest : RestRequest
    {
        public WebminRestRequest(string resource) : base(resource, Method.GET)

        {
        }
    }
}