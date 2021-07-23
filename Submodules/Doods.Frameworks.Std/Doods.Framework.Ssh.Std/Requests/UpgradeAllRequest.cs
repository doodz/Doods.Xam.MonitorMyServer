using System.Collections.Generic;

namespace Doods.Framework.Ssh.Std.Requests
{
    public class UpgradeAllRequest : SshRequestBase
    {
        public const string RequestString = "apt-get upgrade -y";

        public UpgradeAllRequest() : base(RequestString)
        {
            NeedGroup = new List<string> {"root", "sudo"};
            UseSudo = true;
        }
    }
}