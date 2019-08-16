using System;
using System.Collections.Generic;
using System.Text;
using Doods.Framework.Ssh.Std.Requests;
using Doods.Framework.Ssh.Std.Serializers;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class SystemInformationRequest : OmvRequestBase
    {

        public const string RequestString = "omv-rpc System getInformation";
        public SystemInformationRequest() : base(RequestString)
        {
        }

    }
}
