using RestSharp;

namespace Doods.Synology.Webapi.Std
{
    internal class SynologyRestRequest : RestRequest
    {
        public SynologyRestRequest(string resource) : base(resource, Method.GET)

        {
        }
    }
}