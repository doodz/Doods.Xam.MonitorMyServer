namespace Doods.Framework.Ssh.Std.Requests
{
    public class DiskUsageRequest : SshRequestBase
    {
        public const string RequestString = "LC_ALL=C df -h";

        public DiskUsageRequest() : base(RequestString)
        {
        }
    }
}