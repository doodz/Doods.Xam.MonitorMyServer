using Doods.Framework.Ssh.Std.Requests;

namespace Doods.Openmedivault.Ssh.Std.Requests
{
    public class InstallOMVExtra : SshRequestBase
    {
        public const string RequestString =
            "wget -O - https://github.com/OpenMediaVault-Plugin-Developers/packages/raw/master/install | bash";

        public InstallOMVExtra(bool withSudo) : base(RequestString)
        {
            UseSudo = withSudo;
        }
    }
}