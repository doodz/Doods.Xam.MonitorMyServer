namespace Doods.Framework.Ssh.Std.Requests
{
    public class RebootRequest : SshRequestBase
    {
        //public const string RequestString = "echo \"{0}\" | sudo -S /sbin/shutdown -r now";
        public const string RequestString = "/sbin/shutdown -r now";

        public RebootRequest(bool withSudo) : base(RequestString)
        {
            UseSudo = withSudo;
        }
    }
}