namespace Doods.Framework.Ssh.Std.Requests
{
    public class HaltSignalRequest : SshRequestBase
    {
        //public const string RequestString = "echo \"{0}\" | sudo -S /sbin/shutdown -h now";
        public const string RequestString = "/sbin/shutdown -h now";

        public HaltSignalRequest(bool withSudo) : base(RequestString)
        {
            UseSudo = withSudo;
        }
    }
}