using System.Collections.Generic;

namespace Doods.Framework.Ssh.Std.Requests
{
    public class AptUpdateRequest : SshRequestBase
    {
        public const string RequestString = "apt update";

        public AptUpdateRequest(bool withSudo) : base(RequestString)
        {
            NeedGroup = new List<string> {"root", "sudo"};
            UseSudo = withSudo;
        }
    }
}