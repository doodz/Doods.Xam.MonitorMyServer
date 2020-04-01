using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;

namespace Doods.Synology.Webapi.Std
{
    internal class SynologyRestRequest : RestRequest
    {
        public SynologyRestRequest(string resource):base(resource, Method.GET)
           
        {
        }
    }
}
