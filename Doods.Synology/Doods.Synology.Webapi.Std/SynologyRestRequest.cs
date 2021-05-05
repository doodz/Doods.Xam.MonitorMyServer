using System.Security.Cryptography.X509Certificates;
using RestSharp;

namespace Doods.Synology.Webapi.Std
{
    internal class SynologyRestRequest : RestRequest
    {
        public SynologyRestRequest(string resource) : base(resource, Method.GET)

        {


          
        }

        public SynologyRestRequest AddArrayParameter(string parameterName, params string[] arrayVal)
        {
            var array = new JsonArray();
            foreach (var s in arrayVal)
            {
                array.Add(s);
            }

            AddParameter(parameterName, array);
            return this;
        }
    }
}