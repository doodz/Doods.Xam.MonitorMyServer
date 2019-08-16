using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Ssh.Std.Requests;
using Newtonsoft.Json;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class DevicesRequest : OmvRequestBase
    {

        public const string RequestString = "omv-rpc Network enumerateDevicesList \"{\\\"start\\\":0,\\\"limit\\\":25}\"";
        public DevicesRequest() : base(RequestString)
        {
         
        }

    }
}