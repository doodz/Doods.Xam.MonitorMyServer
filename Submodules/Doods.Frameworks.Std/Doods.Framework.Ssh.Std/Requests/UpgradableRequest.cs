namespace Doods.Framework.Ssh.Std.Requests
{
    public class UpgradableRequest : SshRequestBase
    {
        public const string RequestString = "apt list --upgradable";

        public UpgradableRequest() : base(RequestString)
        {
        }
    }
}