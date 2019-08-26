using System;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class UpdateAptListRequest : OmvRequestBase
    {
        public const string RequestString = "omv-rpc Apt update";
        public UpdateAptListRequest() : base(RequestString)
        {

        }
    }
}